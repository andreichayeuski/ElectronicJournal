<template>
    <div id="administration">
        <div v-show="groupsShowed">
            <div class="title">
                <!--<label class="header">Группы</label>-->
                <button v-on:click="addGroup()" class="btn btn-dark">Добавить группу</button>
                <!--<button v-on:click="showUsers()" class="btn btn-dark">Перечень пользователей</button>-->
            </div>
            <div v-for="group, i in groups" class="group">
                <div class="edit-div" v-if="editGroup === group.id">
                    <div class="form-row">
                        <label class="label col-form-label">Курс</label>
                        <span class="form-control">3</span>
                        <button class="btn btn-dark save" v-on:click="editGroup === 0 ? saveGroup(group) : updateGroup(group)">Сохранить</button>
                        <button class="btn btn-dark save" v-on:click="undoChangesGroup(group)">Отмена</button>
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Номер группы</label>
                        <input class="form-control" type="number" min="1" max="10" v-model="group.number" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Подгруппа</label>
                        <select class="form-control" v-model="group.halfGroup">
                            <option class="form-control" disabled value="">Выберите один из вариантов</option>
                            <option class="form-control" value="false">1</option>
                            <option class="form-control" value="true">2</option>
                        </select>
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Дата начала обучения</label>
                        <input class="form-control" v-model="group.startDate" type="date" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Дата конца обучения</label>
                        <input v-on:keyup.13="editGroup === 0 ? saveGroup(group) : updateGroup(group)" v-model="group.endDate" type="date" class="form-control" />
                    </div>
                </div>
                <div v-else>
                    <label class="label">Курс</label>
                    <span>{{group.courseId}}</span>
                    <br />
                    <label class="label">Номер группы</label>
                    <span>{{group.number}}</span>
                    <br />
                    <label class="label">Подгруппа</label>
                    <span>{{(group.halfGroup + 1)}}</span>
                    <br />
                    <label class="label">Дата начала обучения</label>
                    <span>{{group.startDate}}</span>
                    <br />
                    <label class="label last">Дата конца обучения</label>
                    <span>{{group.endDate}}</span>
                    <button class="btn-sm btn-dark" v-on:click="editGroup = group.id">Изменить</button>
                    <button class="btn-sm btn-dark" v-on:click="deleteGroup(group.id, i)">x</button>
                </div>
            </div>
        </div>
        <!--<div v-show="usersShowed">
            <div class="title">
                <label class="header">Пользователи</label>
                <button v-on:click="addUser()" class="btn btn-dark">Добавить пользователя</button>
                <button v-on:click="showGroups()" class="btn btn-dark">Перечень групп</button>
            </div>
            <div class="user" v-for="user, i in users">
                <div class="edit-div" v-if="editUser === user.id">
                    <div class="form-row">
                        <label class="label col-form-label">Фамилия</label>
                        <input class="form-control" v-model="user.sName" />
                        <button class="btn btn-dark save" v-on:click="editUser === 0 ? saveUser(user) : updateUser(user)">Сохранить</button>
                        <button class="btn btn-dark save" v-on:click="undoChangesUser(user)">Отмена</button>
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Имя</label>
                        <input class="form-control" v-model="user.fName" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Отчество</label>
                        <input class="form-control" v-model="user.mName" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Дата рождения</label>
                        <input v-model="user.birthDay" type="date" class="form-control" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Email</label>
                        <input type="email" class="form-control" v-model="user.email" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Персональный номер</label>
                        <input class="form-control" v-model="user.personalNumber" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Номер группы</label>
                        <select class="form-control" v-model="user.groupId">
                            <option v-for="group in groups" v-bind:value="group.id">
                                {{group.number + '/' + (group.halfGroup + 1)}}
                            </option>
                        </select>
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Роль</label>
                        <select class="form-control" v-model="user.roleName">
                            <option class="form-control" disabled value="">Выберите один из вариантов</option>
                            <option class="form-control" value="Студент">Студент</option>
                            <option class="form-control" value="ЗаместительCтаросты">Заместитель старосты</option>
                            <option class="form-control" value="Староста">Староста</option>
                            <option class="form-control" value="Администратор">Администратор</option>
                        </select>
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Дата начала обучения</label>
                        <input class="form-control" v-model="user.startDate" type="date" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Дата конца обучения</label>
                        <input v-model="user.removalDate" type="date" class="form-control" />
                    </div>
                    <div class="form-row">
                        <label class="label col-form-label">Пол</label>
                        <span class="radiobtn"><input v-model="user.sex" type="radio" value="false"> Женский  </span>
                        <span class="radiobtn"><input v-model="user.sex" type="radio" value="true"> Мужской</span>
                    </div>
                </div>
                <div v-else>
                    <label class="label">Фамилия</label>
                    <span>{{user.sName}}</span>
                    <br />
                    <label class="label">Имя</label>
                    <span>{{user.fName}}</span>
                    <br />
                    <label class="label">Отчество</label>
                    <span>{{user.mName}}</span>
                    <br />
                    <label class="label">Дата рождения</label>
                    <span>{{user.birthDay}}</span>
                    <br />
                    <label class="label">Email</label>
                    <span>{{user.email}}</span>
                    <br />
                    <label class="label">Персональный номер</label>
                    <span>{{user.personalNumber}}</span>
                    <br />
                    <label class="label">Группа</label>
                    <span>{{user.group}}</span>
                    <br />
                    <label class="label">Роль</label>
                    <span>{{user.roleName}}</span>
                    <br />
                    <label class="label">Пол</label>
                    <span>{{user.sexView}}</span>
                    <br />
                    <label class="label">Дата начала обучения</label>
                    <span>{{user.startDate}}</span>
                    <br />
                    <label class="label">Дата конца обучения</label>
                    <span>{{user.removalDate}}</span>
                    <button class="btn-sm btn-dark" v-on:click="editUser = user.id">Изменить</button>
                    <button class="btn-sm btn-dark" v-on:click="deleteUser(user.id, i)">x</button>
                </div>
            </div>
        </div>-->
    </div>
</template>

<script>
    import axios from 'axios'
    import moment from 'moment'
    export default {
        name: 'group',
        props: ['url'],
        data() {
            return {
                groupsShowed: false,
                usersShowed: false,
                editGroup: null,
                editUser: null,
                groups: [],
                courses: [],
                users: []
            }
        },
        methods: {
            addGroup() {
                this.groups.unshift({ id: 0 });
                this.editGroup = 0;
            },
            deleteGroup(id, i) {
                fetch("/api/group/" + id, {
                    method: "DELETE"
                })
                    .then(() => {
                        this.groups.splice(i, 1);
                    })
            },
            updateGroup(group) {
                group.halfGroup = JSON.parse(group.halfGroup);
                let isValid = this.validateGroup(group);
                if (isValid !== '')
                    alertify.error(isValid);
                else {
                    group.courseId = 1;
                    fetch("/api/group/" + group.id, {
                        body: JSON.stringify(this.castFieldsToNumbers(group)),
                        method: "PUT",
                        headers: {
                            "Content-Type": "application/json",
                        },
                    })
                        .then(() => {
                            this.editGroup = null;
                            this.getGroups();
                            alertify.success("Успешно!");
                        })
                }
            },
            saveGroup(group) {
                group.halfGroup = JSON.parse(group.halfGroup);
                let isValid = this.validateGroup(group);
                if (isValid !== '')
                    alertify.error(isValid);
                else {
                    group.courseId = 1;
                    axios.post("/api/group", this.castFieldsToNumbers(group))
                        .then((resp) => {
                            this.editGroup = null;
                            this.getGroups();
                            alertify.success("Успешно!");
                        })
                }
            },
            updateCoursesNumbers(coursesIds) {
                var editGroups = this.groups;
                coursesIds.forEach(function (course) {
                    axios
                        .get("/api/course/" + course)
                        .then(response => {
                            course = response.data;
                            editGroups.forEach(function (group) {
                                if (group.courseId === course.id) {
                                    group.courseId = course.number;
                                }
                            });
                        })
                });
                this.groups = editGroups;
            },
            validateGroup(group) {
                let error = '';
                if (group.number === undefined)
                    error += 'Не введён номер группы.\n';
                if (group.halfGroup === undefined)
                    error += 'Не выбрана подгруппа.\n';
                if (group.startDate == undefined)
                    error += 'Не задана дата начала обучения.\n';
                for (let i = 0; i < this.groups.length; i++) {
                    if (this.groups[i].id !== group.id
                        && this.groups[i].number == group.number
                        && this.groups[i].halfGroup == group.halfGroup) {
                        error += 'Такая группа уже существует.\n';
                    }
                }
                return error;
            },
            castFieldsToNumbers(group) {
                group.number = Number(group.number);
                return group;
            },
            getGroups() {
                axios
                    .get('/api/group')
                    .then(response => {
                        this.groups = response.data;
                        let coursesIds = [];
                        this.groups.forEach(function (item) {
                            if (!coursesIds.includes(item.courseId)) {
                                coursesIds.push(item.courseId);
                            }
                            item.startDate = moment(item.startDate).format('YYYY-MM-DD');
                            if (item.endDate !== null) {
                                item.endDate = moment(item.endDate).format('YYYY-MM-DD');
                            }
                        });
                        this.updateCoursesNumbers(coursesIds);
                        this.getUsers();
                    })
                    .catch(error => (console.log(error)));
            },
            undoChangesGroup(group) {
                this.editGroup = null;
                if (group.id === 0) {
                    this.groups.shift();
                }
            },
            getUsers() {
                axios
                    .get('/api/user')
                    .then(response => {
                        this.users = response.data;
                        let groups = this.groups;
                        this.users.forEach(function (item) {
                            item.sexView = item.sex ? 'Мужской' : 'Женский';
                            item.birthDay = moment(item.birthDay).format('YYYY-MM-DD');
                            item.startDate = moment(item.startDate).format('YYYY-MM-DD');
                            if (item.removalDate !== null) {
                                item.removalDate = moment(item.endDate).format('YYYY-MM-DD');
                            }
                            groups.forEach(function (group) {
                                if (group.id === item.groupId) {
                                    item.group = group.number + '/' + (group.halfGroup + 1);
                                }
                            })
                        });
                    })
                    .catch(error => (console.log(error)));
            },
            showUsers() {
                this.groupsShowed = false;
                this.usersShowed = true;
                return 0;
            },
            showGroups() {
                this.usersShowed = false;
                this.groupsShowed = true;
                return 0;
            },
            deleteUser(id, i) {
                fetch("/api/user/" + id, {
                    method: "DELETE"
                })
                    .then(() => {
                        this.users.splice(i, 1);
                        alertify.success("Успешно!");
                    })
            },
            updateUser(user) {
                /*let isValid = this.validateGroup(group);
                if (isValid !== '')
                    alertify.error(isValid);
                else {*/
                fetch("/api/user/" + user.id, {
                    body: JSON.stringify(user),
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                })
                    .then(() => {
                        this.editUser = null;
                        this.getUsers();
                        alertify.success("Успешно!");
                    })
                //}
            },
            saveUser(user) {
                /*let isValid = this.validateGroup(group);
                if (isValid !== '')
                    alertify.error(isValid);
                else {*/
                axios.post("/api/user", user)
                    .then((resp) => {
                        this.editUser = null;
                        this.getUsers();
                        alertify.success("Успешно!");
                    })
                //}
            },
            undoChangesUser(user) {
                this.editUser = null;
                if (user.id === 0) {
                    this.users.shift();
                }
            }
        },
        mounted: function () {
            this.groupsShowed = true;
            this.coursesShowed = false;
            this.usersShowed = false;
            this.getGroups();
        }
    }
</script>
