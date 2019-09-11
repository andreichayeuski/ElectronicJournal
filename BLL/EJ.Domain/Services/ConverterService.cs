using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using EJ.Entities.Models;
using EJ.Models.Enums;
using EJ.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SHARED.Common.Extensions;
using Group = EJ.Entities.Models.Group;

namespace EJ.Domain.Services
{
    public interface IConverterService
    {
        ObjectResult ParseSheduleFromFile(Stream fileStream);
    }
    public class ConverterService : IConverterService
    {
        private readonly IRepository<Auditorium> _auditoriumRepository;
        private readonly IRepository<TimeSpending> _timeSpendingRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<GroupShedule> _groupSheduleRepository;
        private readonly IRepository<Semester> _semesterRepository;
        private readonly IRepository<SheduleSubject> _sheduleSubjectRepository;
        private readonly IRepository<SheduleTimeSpending> _sheduleTimeSpendingRepository;
        private readonly IRepository<Subject> _subjectRepository;
        private readonly IRepository<WeekDay> _weekDayRepository;
        public ConverterService(IRepository<Auditorium> auditoriumRepository,
            IRepository<TimeSpending> timeSpendingRepository,
            IRepository<Group> groupRepository, IRepository<GroupShedule> groupSheduleRepository,
            IRepository<SheduleSubject> sheduleSubjectRepository,
            IRepository<SheduleTimeSpending> sheduleTimeSpendingRepository,
            IRepository<Subject> subjectRepository, IRepository<Semester> semesterRepository,
            IRepository<WeekDay> weekDayRepository)
        {
            _auditoriumRepository = auditoriumRepository;
            _timeSpendingRepository = timeSpendingRepository;
            _groupRepository = groupRepository;
            _groupSheduleRepository = groupSheduleRepository;
            _sheduleSubjectRepository = sheduleSubjectRepository;
            _sheduleTimeSpendingRepository = sheduleTimeSpendingRepository;
            _subjectRepository = subjectRepository;
            _semesterRepository = semesterRepository;
            _weekDayRepository = weekDayRepository;
        }

        private int GetNumberFromRoman(string romanNumber)
        {
            foreach (var number in (RomanNumbersEnum[]) Enum.GetValues(typeof(RomanNumbersEnum)))
            {
                var e = number;
                var q = number.ToString();
                if (romanNumber == number.ToString())
                {
                    return (int) number;
                }
            }
            return 0;
        }

        private ClassTypeEnum GetClassTypeEnumValue(string stringValue)
        {
            foreach (var classType in (ClassTypeEnum[]) Enum.GetValues(typeof(ClassTypeEnum)))
            {
                if (stringValue == classType.ToString())
                {
                    return classType;
                }
            }
            return ClassTypeEnum.но;
        }

        private bool ContainsTeacherDegree(string stringValue)
        {
            foreach (var classType in (TeacherDegreeEnum[]) Enum.GetValues(typeof(TeacherDegreeEnum)))
            {
                if (stringValue == classType.GetDescription())
                {
                    return true;
                }
            }
            return false;
        }

        private string GetStringValueFromCell(IXLCell cell)
        {
            if (cell.IsMerged())
            {
                return cell.MergedRange().Cell(1, 1).GetString();
            }
            return cell.GetString();
        }
        public ObjectResult ParseSheduleFromFile(Stream fileStream)
        {
            try
            {
                XLWorkbook workbook = new XLWorkbook(fileStream);
                var worksheet = workbook.Worksheet(1);
                var firstRowUsed = worksheet.FirstRowUsed();
                var excelRow = firstRowUsed.RowBelow(5);
                var groups = GetGroupsFromFile(excelRow);
                excelRow = excelRow.RowBelow();
                var semesterInfoCellValue = worksheet.Cell("G3");
                var semesterInfo = ParseSemesterInfoCell(semesterInfoCellValue);
                var autumnSemester = semesterInfo[1] == "осенний";
                var yearSemester = semesterInfo[2].Substring(0, 4);
                var semester = _semesterRepository.FindFirst(x => x.StartDate.Year.ToString() == yearSemester
                    && x.StartDate.Month == (autumnSemester ? 9 : 2));
                if (semester == null)
                {
                    semester = _semesterRepository.Add(new Semester
                    {
                        StartDate = autumnSemester ? DateTime.Parse("01.09." + yearSemester)
                            : DateTime.Parse("01.02." + yearSemester),
                        EndDate = autumnSemester ? DateTime.Parse("25.12." + yearSemester)
                            : DateTime.Parse("10.06." + (int.Parse(yearSemester) + 1))
                    });
                }
                while (GetStringValueFromCell(excelRow.Cell(1)) != "break")
                {
                    groups = ParseRow(semester, groups, excelRow);
                    excelRow = excelRow.RowBelow();
                }
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex)
                {
                    StatusCode = 500
                };
            }
        }
        private List<string> ParseSemesterInfoCell(IXLCell cellValue)
        {
            var result = new List<string>();
            foreach (var word in cellValue.RichText)
            {
                if (word.Italic && word.Text.Contains("I"))
                {
                    result.Add(word.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);
                }
                if (!word.Italic && word.Underline == XLFontUnderlineValues.Single && word.Text != " ")
                {
                    result.Add(word.Text);
                }
                if (word.Text.Contains('-'))
                {
                    result.Add(word.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
                }
            }
            return result;
        }

        private List<Group> GetGroupsFromFile(IXLRow row)
        {
            var groups = new int[row.LastCellUsed().Address.ColumnNumber - 2];
            var resultGroups = new List<Group>();
            for (int i = 3; i < row.LastCellUsed().Address.ColumnNumber - 2; i++)
            {
                var dataFromRow = row.Cell(i).GetString();
                if (dataFromRow != "")
                {
                    groups[i] = int.Parse(dataFromRow.Split(' ')[0]);
                    resultGroups.Add(_groupRepository.FindFirst(x => x.Number == groups[i]
                        && !x.HalfGroup));
                }
                else
                {
                    resultGroups.Add(_groupRepository.FindFirst(x => x.Number == groups[i - 1]
                        && x.HalfGroup));
                }
            }
            return resultGroups;
        }

        private List<Group> ParseRow(Semester semester, List<Group> groups, IXLRow row)
        {
            var timeSplit = GetStringValueFromCell(row.Cell(2)).Split(new char[] { '.', '-' },
                StringSplitOptions.RemoveEmptyEntries);
            if (timeSplit.Count() >= 2)
            {
                try
                {
                    var timeSpending = _timeSpendingRepository.FindFirst(x => x.StartTime.Minutes == int.Parse(timeSplit[1])
                        && x.StartTime.Hours == int.Parse(timeSplit[0]));
                    if (timeSpending != null)
                    {
                        var day = GetStringValueFromCell(row.Cell(1));
                        var week = 0;
                        if (row.Cell(2).IsMerged())
                        {
                            week = row.Cell(2).GetString() != "" ? 1 : 2;
                        }
                        var weekDaysFromRepository = _weekDayRepository.Find(x => x.Day.ToLower() == day
                            && (week == 0 ? true : x.NumberOfWeek == week));

                        for (int i = 3; i < row.LastCellUsed().Address.ColumnNumber - 2; i++)
                        {
                            var cellValue = Regex.Replace(GetStringValueFromCell(row.Cell(i)).Trim(), @"\s+", " ");
                            if (cellValue != "" && cellValue != "I" && cellValue != "II")
                            {
                                var subjectInfo = ParseCellValue(cellValue);
                                if (subjectInfo != null)
                                {
                                    foreach (var subjectItem in subjectInfo)
                                    {
                                        var subject = new Subject();

                                        subject = _subjectRepository.FindFirst(x =>
                                             x.Name == subjectItem.SubjectName || x.ShortName == subjectItem.SubjectName);

                                        if (subject != null)
                                        {
                                            if (subject.Name == null && subjectItem.ClassType == ClassTypeEnum.лк)
                                            {
                                                subject.Name = subjectItem.SubjectName;
                                                subject = _subjectRepository.Update(subject);
                                            }
                                            if (subject.ShortName == null && subjectItem.ClassType != ClassTypeEnum.лк)
                                            {
                                                subject.ShortName = subjectItem.SubjectName;
                                                subject = _subjectRepository.Update(subject);
                                            }
                                        }
                                        else
                                        {
                                            subject = _subjectRepository.Add(new Subject
                                            {
                                                Name = subjectItem.ClassType == ClassTypeEnum.лк
                                                            ? subjectItem.SubjectName : null,
                                                ShortName = subjectItem.ClassType == ClassTypeEnum.лк
                                                            ? null : subjectItem.SubjectName
                                            });
                                        }
                                        var groupShedule = _groupSheduleRepository.FindFirst(x => x.GroupId == groups[i - 3].Id)
                                            ?? _groupSheduleRepository.Add(new GroupShedule
                                            {
                                                GroupId = groups[i - 3].Id,
                                                SemesterId = semester.Id
                                            });
                                        var sheduleSubject = _sheduleSubjectRepository.FindFirst(x => x.SubjectId == subject.Id
                                            && x.GroupSheduleId == groupShedule.Id)
                                            ?? _sheduleSubjectRepository.Add(new SheduleSubject
                                            {
                                                SubjectId = subject.Id,
                                                GroupSheduleId = groupShedule.Id
                                            });
                                        var auditorium = _auditoriumRepository.FindFirst(x => x.Number == subjectItem.AuditoruimNumber)
                                            ?? _auditoriumRepository.Add(new Auditorium
                                            {
                                                Number = subjectItem.AuditoruimNumber
                                            });


                                        var sheduleTimeSpending = new List<SheduleTimeSpending>();
                                        sheduleTimeSpending.AddRange(_sheduleTimeSpendingRepository.Find(x =>
                                            x.ClassTypeId == (int) subjectItem.ClassType
                                            && x.AuditoriumId == auditorium.Id
                                            && x.SheduleSubjectId == sheduleSubject.Id
                                            && x.TimeSpendingId == timeSpending.Id
                                            && (week == 0 ? weekDaysFromRepository.Select(y => y.Id).Contains(x.WeekDayId)
                                                : weekDaysFromRepository.Where(y => y.NumberOfWeek == week).Select(y => y.Id).Contains(x.WeekDayId))));
                                        if (sheduleTimeSpending.Count == 0)
                                        {
                                            foreach (var weekDay in weekDaysFromRepository)
                                            {
                                                sheduleTimeSpending.Add(_sheduleTimeSpendingRepository.Add(new SheduleTimeSpending
                                                {
                                                    ClassTypeId = (int) subjectItem.ClassType,
                                                    AuditoriumId = auditorium.Id,
                                                    SheduleSubjectId = sheduleSubject.Id,
                                                    TimeSpendingId = timeSpending.Id,
                                                    WeekDayId = weekDay.Id
                                                }));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    return null;
                }
            }
            return groups;
        }

        private List<SheduleCellInfo> ParseCellValue(string cellValue)
        {
            var listSheduleCellInfo = new List<SheduleCellInfo>();
            var cellValueSplited = cellValue.Split(new string[] { " ", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (cellValueSplited[0] == "Физическая")
            {
                listSheduleCellInfo.Add(new SheduleCellInfo
                {
                    WeekNumber = 0,
                    ClassType = ClassTypeEnum.пз,
                    AuditoruimNumber = "",
                    SubjectName = "Физическая культура"
                });
                return listSheduleCellInfo;
            }
            var firstValueRomanNumber = cellValueSplited[0] == "I" || cellValueSplited[0] == "II";
            var subjectName = "";
            var weekNumber = firstValueRomanNumber ? GetNumberFromRoman(cellValueSplited[0]) : 0;
            var classType = firstValueRomanNumber ? GetClassTypeEnumValue(cellValueSplited[1])
                        : GetClassTypeEnumValue(cellValueSplited[0]);
            var auditoruimNumber = firstValueRomanNumber ? cellValueSplited[2] : cellValueSplited[1];
            var indexComment = 0;
            for (int i = 0; i < cellValueSplited.Length; i++)
            {
                if (cellValueSplited[i].Contains("("))
                {
                    indexComment = i;
                }
            }
            for (int i = firstValueRomanNumber ? 3 : 2; i + (indexComment != 0 ? (cellValueSplited.Length - indexComment) : 0) < cellValueSplited.Length; i++)
            {
                if (GetClassTypeEnumValue(cellValueSplited[i]) != ClassTypeEnum.но)
                {
                    listSheduleCellInfo.Add(new SheduleCellInfo
                    {
                        WeekNumber = weekNumber,
                        ClassType = classType,
                        AuditoruimNumber = auditoruimNumber,
                        SubjectName = subjectName.TrimEnd(' ', '/')
                    });
                    subjectName = "";
                    for (int j = i + 2; j + 3 < cellValueSplited.Length; j++)
                    {
                        subjectName += cellValueSplited[j] + " ";
                    }
                    listSheduleCellInfo.Add(new SheduleCellInfo
                    {
                        WeekNumber = 0,
                        ClassType = GetClassTypeEnumValue(cellValueSplited[i]),
                        AuditoruimNumber = cellValueSplited[i + 1],
                        SubjectName = subjectName.TrimEnd(' ', '/')
                    });
                    break;
                }
                if (!ContainsTeacherDegree(cellValueSplited[i]))
                {
                    subjectName += cellValueSplited[i] + " ";
                }
                else
                {
                    i += 2;
                    subjectName += "/ ";
                }
            }
            if (listSheduleCellInfo.Count == 0)
            {
                listSheduleCellInfo.Add(new SheduleCellInfo
                {
                    WeekNumber = firstValueRomanNumber ? GetNumberFromRoman(cellValueSplited[0]) : 0,
                    ClassType = firstValueRomanNumber ? GetClassTypeEnumValue(cellValueSplited[1])
                        : GetClassTypeEnumValue(cellValueSplited[0]),
                    AuditoruimNumber = firstValueRomanNumber ? cellValueSplited[2] : cellValueSplited[1],
                    SubjectName = subjectName.TrimEnd(' ', '/'),
                    Comment = cellValueSplited[cellValueSplited.Length - 1].Replace("(", "").Replace(")", "")
                });
            }
            return listSheduleCellInfo;
        }

        private class SheduleCellInfo
        {
            public int WeekNumber { get; set; }
            public ClassTypeEnum ClassType { get; set; }
            public string AuditoruimNumber { get; set; }
            public string SubjectName { get; set; }
            public string Comment { get; set; }
        }
    }
}
