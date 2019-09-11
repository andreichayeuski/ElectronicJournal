<template>
    <div>
        <!--<b-button @click="addRow" class="whiteBtn" v-bind:disabled="IsPreventEditLosses">Добавить</b-button>-->
        <b-table class="infoTable" :fields="fields" :items="items">
            <!--<template slot="index" slot-scope="data">
                {{data.index + 1}}
            </template>-->

            <template slot="Id" slot-scope="data">
                <span v-if="isUrlOrStatusEqualsRegistred(data.item.StatusId)">
                    <a :href="'OperationalIncident/OperationalIncidentsAction/Edit?id=' + data.item.Id" :data-main-menu-parentid="'OperationalIncidents'" :data-menu-tab-id="'fakeTabOperationIncidentId' + data.item.Id" data-menu-tab="" :data-menu-tab-name="operationalIncidentTabName(data.item.Number)" data-menu-tab-renew-id="false">{{operationalIncidentName(data.item.Number)}}</a>
                </span>
                <span v-else>
                    {{operationalIncidentName(data.item.Number)}}
                </span>
            </template>
            <!--Магическое число 2 это NOT INCIDENT в справочнике Dictionary.G11.IncidentAttributes-->
            <template slot="Department" slot-scope="data" v-if="data.item.IncidentAttributeId != 2">
                {{data.item.Department}}
            </template>
            <template slot="Status" slot-scope="data">
                {{data.item.Status}}
            </template>
            <template slot="IncidentAttribute" slot-scope="data">
                {{data.item.IncidentAttribute}}
            </template>

            <!--<template slot="actions" slot-scope="data">
            <b-btn @click="removeRow(data.index)" class="whiteBtn">×</b-btn>
            </template>-->
        </b-table>
    </div>
</template>

<script>
    export default {
        props: ['RiskEventIncidentsItems', 'isUrm'],
        data() {
            return {
                sortDesc: false,
                fields: [
                    //{ label: '№', key: 'index' },
                    { label: '№ инцидента', key: 'Id' },
                    { label: 'Владелец объекта риска', key: 'Department' },
                    { label: 'Статус', key: 'Status' },
                    { label: 'Признак', key: 'IncidentAttribute' },
                    //{ label: '', key: 'actions' }
                ],
                items: this.RiskEventIncidentsItems,
                tempItems: {},
            }
        },
        mounted: function () {
            EJ.Utils.WrapTabs($(".vueAjaxLink"));
        },
        methods: {
            addRow: function () {
                this.items.push({
                    Amount: null,
                    Comment: "",
                    FinancialLossStatusId: null,
                    FinancialLossTypeId: null,
                    Id: 0
                });
            },
            isUrlOrStatusEqualsRegistred: function (statusId) {
                //Магическое число 2 это статус ои Зарегистрирован
                return this.isUrm || statusId == 2;
            },
            operationalIncidentName: function (value) {
                if (value == null || value == undefined || value.length == 0)
                    return 'Б/Н';
                return value;
            },
            operationalIncidentTabName: function (value) {
                if (value == null || value == undefined || value.length == 0)
                    value = 'Б/Н';
                return value + '. ОИ';
            },
            removeRow: function (index) {
                this.items.splice(index, 1);
            },
        },
        watch: {
            items: {
                handler: function () {
                    this.$emit('update', this.items)
                },
                deep: true
            }
            , IsPreventEditLosses: {
                handler: function (newValue, oldValue) {
                    if (newValue === true && oldValue === false) {
                        this.items = this.tempItems;
                        this.$emit('update', this.items);
                    }
                    else if (newValue === false && oldValue === true) {
                        var cloned = this.items != undefined ? JSON.parse(JSON.stringify(this.items)) : this.items;
                        this.tempItems = cloned;
                    }
                }
            }
        }
    }
</script>
