using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal static class Util
	{
		internal class SearchArguments
		{
			public string SearchColumn
			{
				get;
				set;
			}

			public string SearchString
			{
				get;
				set;
			}

			public SearchOperation SearchOperation
			{
				get;
				set;
			}
		}

		internal static JsonTreeResponse PrepareJsonTreeResponse(JsonTreeResponse response, CoreGrid grid, List<Hashtable> data)
		{
			for (int i = 0; i < data.Count; i++)
			{
				for (int j = 0; j < grid.Columns.Count; j++)
				{
					CoreColumn coreColumn = grid.Columns[j];
					if (!string.IsNullOrEmpty(coreColumn.DataField) && !string.IsNullOrEmpty(coreColumn.DataFormatString))
					{
						data[i][coreColumn.DataField] = coreColumn.FormatDataValue(data[i][coreColumn.DataField], coreColumn.HtmlEncode);
					}
				}
				response.rows[i] = data[i];
			}
			return response;
		}

		internal static JsonResult ConvertToJson(JsonTreeResponse response, CoreGrid grid, List<Hashtable> data)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			return new JsonResult((object)PrepareJsonTreeResponse(response, grid, data));
		}

		public static int GetPrimaryKeyIndex(CoreGrid grid)
		{
			foreach (CoreColumn column in grid.Columns)
			{
				if (column.PrimaryKey)
				{
					return grid.Columns.IndexOf(column);
				}
			}
			return 0;
		}

		public static string GetPrimaryKeyField(CoreGrid grid)
		{
			int primaryKeyIndex = GetPrimaryKeyIndex(grid);
			return grid.Columns[primaryKeyIndex].DataField;
		}

		public static CoreColumn GetPrimaryKeyColumn(CoreGrid grid)
		{
			int primaryKeyIndex = GetPrimaryKeyIndex(grid);
			return grid.Columns[primaryKeyIndex];
		}

		public static Hashtable GetFooterInfo(CoreGrid grid)
		{
			Hashtable hashtable = new Hashtable();
			if (grid.AppearanceSettings.ShowFooter)
			{
				foreach (CoreColumn column in grid.Columns)
				{
					hashtable[column.DataField] = column.FooterValue;
				}
			}
			return hashtable;
		}

		public static string GetWhereClause(CoreGrid grid, Dictionary<string, string> queryString)
		{
			string text = " && ";
			string text2 = "";
			Hashtable hashtable = new Hashtable();
			foreach (CoreColumn column in grid.Columns)
			{
				if (queryString.TryGetValue(column.DataField, out string value))
				{
					SearchArguments args = new SearchArguments
					{
						SearchColumn = column.DataField,
						SearchString = value,
						SearchOperation = column.SearchToolBarOperation
					};
					string str = (text2.Length > 0) ? text : "";
					string str2 = ConstructLinqFilterExpression(grid, args);
					text2 = text2 + str + str2;
				}
			}
			return text2;
		}

		public static string GetWhereClause(CoreGrid grid, string searchField, string searchString, string searchOper)
		{
			string text = " && ";
			string text2 = "";
			Hashtable hashtable = new Hashtable();
			SearchArguments args = new SearchArguments
			{
				SearchColumn = searchField,
				SearchString = searchString,
				SearchOperation = GetSearchOperationFromString(searchOper)
			};
			string str = (text2.Length > 0) ? text : "";
			string str2 = ConstructLinqFilterExpression(grid, args);
			return text2 + str + str2;
		}

		public static string GetWhereClause(CoreGrid grid, string filters)
		{
			JsonMultipleSearch jsonMultipleSearch = JsonConvert.DeserializeObject<JsonMultipleSearch>(filters);
			string text = "";
			foreach (MultipleSearchRule rule in jsonMultipleSearch.rules)
			{
				SearchArguments args = new SearchArguments
				{
					SearchColumn = rule.field,
					SearchString = rule.data,
					SearchOperation = GetSearchOperationFromString(rule.op)
				};
				string str = (text.Length > 0) ? (" " + jsonMultipleSearch.groupOp + " ") : "";
				text = text + str + ConstructLinqFilterExpression(grid, args);
			}
			return text;
		}

        #region New version

        //internal static string ConstructLinqFilterExpression(CoreGrid grid, SearchArguments args)
        //{
        //    CoreColumn coreColumn = grid.Columns.Find((CoreColumn c) => c.DataField == args.SearchColumn);
        //    if (coreColumn.DataType == null)
        //    {
        //        throw new DataTypeNotSetException("CoreGridColumn.DataType must be set in order to perform search operations.");
        //    }

        //    string filterExpressionCompare = ((coreColumn.DataType == typeof(string)) ? true : false) ? "{0} {1} \"{2}\"" : "{0} {1} {2}";
        //    if (coreColumn.DataType == typeof(DateTime))
        //    {
        //        DateTime dateTime = DateTime.Parse(args.SearchString);
        //        string str = $"({dateTime.Year},{dateTime.Month},{dateTime.Day})";
        //        filterExpressionCompare = "{0} {1} DateTime" + str;
        //    }
        //    string str2 = $"{args.SearchColumn} != null AND ";
        //    return str2 + GetLinqExpression(filterExpressionCompare, args, coreColumn.SearchCaseSensitive, coreColumn.DataType);
        //}

        //private static string GetLinqExpression(string filterExpressionCompare, SearchArguments args, bool caseSensitive, Type dataType)
        //{
        //    string text = caseSensitive ? args.SearchString : args.SearchString.ToLower();
        //    string arg = args.SearchColumn;
        //    if (dataType != null && dataType == typeof(string) && !caseSensitive)
        //    {
        //        arg = $"{args.SearchColumn}.ToLower()";
        //    }
        //    switch (args.SearchOperation)
        //    {
        //        case SearchOperation.IsEqualTo:
        //            return string.Format(filterExpressionCompare, arg, "=", text);
        //        case SearchOperation.IsNotEqualTo:
        //            return string.Format(filterExpressionCompare, arg, "<>", text);
        //        case SearchOperation.IsLessOrEqualTo:
        //            return string.Format(filterExpressionCompare, arg, "<=", text);
        //        case SearchOperation.IsLessThan:
        //            return string.Format(filterExpressionCompare, arg, "<", text);
        //        case SearchOperation.IsGreaterOrEqualTo:
        //            return string.Format(filterExpressionCompare, arg, ">=", text);
        //        case SearchOperation.IsGreaterThan:
        //            return string.Format(filterExpressionCompare, arg, ">", text);
        //        case SearchOperation.BeginsWith:
        //            return $"{arg}.StartsWith(\"{text}\")";
        //        case SearchOperation.Contains:
        //            return $"{arg}.Contains(\"{text}\")";
        //        case SearchOperation.EndsWith:
        //            return $"{arg}.EndsWith(\"{text}\")";
        //        case SearchOperation.DoesNotBeginWith:
        //            return $"!{arg}.StartsWith(\"{text}\")";
        //        case SearchOperation.DoesNotContain:
        //            return $"!{arg}.Contains(\"{text}\")";
        //        case SearchOperation.DoesNotEndWith:
        //            return $"!{arg}.EndsWith(\"{text}\")";
        //        default:
        //            throw new Exception("Invalid search operation.");
        //    }
        //}

        #endregion

        #region Старая версия

        internal static string ConstructLinqFilterExpression(CoreGrid grid, SearchArguments args)
        {
            var column = grid.Columns.Find(c => c.DataField == args.SearchColumn);
            if (column.DataType == null)
            {
                throw new DataTypeNotSetException("CoreGridColumn.DataType must be set in order to perform search operations.");
            }

            if (!string.IsNullOrEmpty(column.SearchDataField))
            {
                args.SearchColumn = column.SearchDataField;
                if (grid.Columns.Any(c => c.DataField == args.SearchColumn))
                    return ConstructLinqFilterExpression(grid, args);
            }

            string filterExpressionCompare = (column.DataType == typeof(string)) ? "{0} {1} \"{2}\"" : "{0} {1} {2}";

            return (string.Format("{0} != null AND ", args.SearchColumn) + GetLinqExpression(filterExpressionCompare, args, column.SearchCaseSensitive, column.DataType));
        }

        private static string GetLinqExpression(string filterExpressionCompare, SearchArguments args, bool caseSensitive, Type dataType)
        {
            string str = caseSensitive ? args.SearchString : args.SearchString.ToLower();
            string searchColumn = args.SearchColumn;

            if (((dataType != null) && (dataType == typeof(string))) && !caseSensitive)
            {
                searchColumn = string.Format("{0}.ToLower()", args.SearchColumn);
            }

            if (((dataType != null) && ((dataType == typeof(DateTime)) || (dataType == typeof(DateTime?)))))
            {
                DateTime time = DateTime.Parse(args.SearchString);
                DateTime time2 = time.AddDays(1);

                if (args.SearchOperation == SearchOperation.IsEqualTo) //берем текущую дату и след день - промежуток между ними - для поиска, если дата содержит минуты и часы
                {
                    filterExpressionCompare =
                        String.Format("{0} >= DateTime({1},{2},{3}) AND {0} < DateTime({4},{5},{6})",
                            args.SearchColumn, time.Year, time.Month, time.Day,
                            time2.Year, time2.Month, time2.Day);
                    return filterExpressionCompare;
                }

                if (args.SearchOperation == SearchOperation.IsGreaterThan)
                {
                    filterExpressionCompare =
                        String.Format("{0} >= DateTime({1},{2},{3})",
                            args.SearchColumn, time2.Year, time2.Month, time2.Day);
                    return filterExpressionCompare;
                }

                string str2 = string.Format("({0},{1},{2})", time.Year, time.Month, time.Day);
                filterExpressionCompare = "{0} {1} DateTime" + str2;
            }

            switch (args.SearchOperation)
            {
                case SearchOperation.IsEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, "=", str.Replace("\"", "\"\""));//добавляем двойные кавычки, чтобы искало в поиске, если строка содержит двойные кавычки


                case SearchOperation.IsNotEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, "<>", str);

                case SearchOperation.IsLessThan:
                    return string.Format(filterExpressionCompare, searchColumn, "<", str);

                case SearchOperation.IsLessOrEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, "<=", str);

                case SearchOperation.IsGreaterThan:
                    return string.Format(filterExpressionCompare, searchColumn, ">", str);

                case SearchOperation.IsGreaterOrEqualTo:
                    return string.Format(filterExpressionCompare, searchColumn, ">=", str);

                case SearchOperation.BeginsWith:
                    return string.Format("{0}.StartsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.DoesNotBeginWith:
                    return string.Format("!{0}.StartsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.EndsWith:
                    return string.Format("{0}.EndsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.DoesNotEndWith:
                    return string.Format("!{0}.EndsWith(\"{1}\")", searchColumn, str);

                case SearchOperation.Contains:
                    return string.Format("{0}.Contains(\"{1}\")", searchColumn, str);

                case SearchOperation.DoesNotContain:
                    return string.Format("!{0}.Contains(\"{1}\")", searchColumn, str);
                case SearchOperation.IsIn:
                    {
                        return "(" + String.Join(" EJ ", str.Split('|').Select(z => searchColumn + "=\"" + z + "\"")) + ")";
                    }

            }
            throw new Exception("Invalid search operation.");
        }

        #endregion
        internal static string ConstructLinqFilterExpression(CoreAutoComplete autoComplete, SearchArguments args)
	    {
	        Guard.IsNotNull(autoComplete.DataField, "DataField", "must be set in order to perform search operations. If you get this error from search/export method, make sure you setup(initialize) the grid again prior to filtering/exporting.");
	        string filterExpressionCompare = "{0} {1} \"{2}\"";
	        return GetLinqExpression(filterExpressionCompare, args, false, typeof(string));
	    }

        private static SearchOperation GetSearchOperationFromString(string searchOperation)
		{
			switch (searchOperation)
			{
			case "eq":
				return SearchOperation.IsEqualTo;
			case "ne":
				return SearchOperation.IsNotEqualTo;
			case "lt":
				return SearchOperation.IsLessThan;
			case "le":
				return SearchOperation.IsLessOrEqualTo;
			case "gt":
				return SearchOperation.IsGreaterThan;
			case "ge":
				return SearchOperation.IsGreaterOrEqualTo;
			case "in":
				return SearchOperation.IsIn;
			case "ni":
				return SearchOperation.IsNotIn;
			case "bw":
				return SearchOperation.BeginsWith;
			case "bn":
				return SearchOperation.DoesNotBeginWith;
			case "ew":
				return SearchOperation.EndsWith;
			case "en":
				return SearchOperation.DoesNotEndWith;
			case "cn":
				return SearchOperation.Contains;
			case "nc":
				return SearchOperation.DoesNotContain;
			default:
				throw new Exception("Search operation not known: " + searchOperation);
			}
		}

		internal static List<Hashtable> ToListOfHashtables(IQueryable iqueryable, CoreGrid grid)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (object item in iqueryable)
			{
				PropertyInfo[] properties = item.GetType().GetProperties();
				Hashtable hashtable = new Hashtable();
				PropertyInfo[] array = properties;
				foreach (PropertyInfo pi in array)
				{
					Type propertyType = pi.PropertyType;
					if (propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						propertyType = Nullable.GetUnderlyingType(propertyType);
					}
					if (grid.TreeGridSettings.Enabled)
					{
						hashtable.Add(pi.Name, pi.GetValue(item, null));
					}
					else if (grid.Columns.Find((CoreColumn c) => c.DataField == pi.Name) != null)
					{
						hashtable.Add(pi.Name, pi.GetValue(item, null));
					}
				}
				list.Add(hashtable);
			}
			return list;
		}

		internal static List<Hashtable> ToListOfHashtables(IQueryable iqueryable)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (object item2 in iqueryable)
			{
				PropertyInfo[] properties = item2.GetType().GetProperties();
				Hashtable item = new Hashtable();
				PropertyInfo[] array = properties;
				foreach (PropertyInfo propertyInfo in array)
				{
					Type propertyType = propertyInfo.PropertyType;
					if (propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						propertyType = Nullable.GetUnderlyingType(propertyType);
					}
				}
				list.Add(item);
			}
			return list;
		}

	    internal static List<Hashtable> ToListOfHashtables<TDestination>(IQueryable iqueryable)
	    {
	        List<Hashtable> list = new List<Hashtable>();
	        foreach (object item2 in iqueryable)
	        {
	            PropertyInfo[] properties = typeof(TDestination).GetProperties();
	            Hashtable item = new Hashtable();
	            PropertyInfo[] array = properties;
	            foreach (PropertyInfo propertyInfo in array)
	            {
	                Type propertyType = propertyInfo.PropertyType;
	                if (propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
	                {
	                    propertyType = Nullable.GetUnderlyingType(propertyType);
	                }
	                item.Add(propertyInfo.Name, propertyInfo.GetValue(item2));
                }
	            list.Add(item);
	        }
	        return list;
	    }
    }
}
