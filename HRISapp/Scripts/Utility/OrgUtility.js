function alertbargo() {
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

//var mMenuModule = angular.module('ABCMenu', ["smart-table", "ui.bootstrap", "ui.select2"]);
var OrgUtility = angular.module('OrgUtility', ["smart-table", "ui.select2"]);
OrgUtility.controller('OrgUtilityController', ['$scope', '$filter', '$http', function (_s, _f, _h) {

    _s.onLoad = function () {
        _h.get("../OrgUtility/getDirectory").then(function (c) {
            _s.Directory = c.data;
        });
    }

    _s.suggestShort = function (data) {
        if (!data.departmentName) {
            data.shortDepartmentName = "";
            return false;
        }
        data.shortDepartmentName = data.departmentName.match(/\b(\w)/g).join('').substring(0,10);
    }

    _s.numberonly = function (data) {
        if (!data.functionCode) {
            return false;
        }
        data.functionCode = parseInt(JSON.stringify(data.functionCode).substring(0, 4));
        return true;
    }

    _s.addNewDepartment = function (data) {

        if (!_s.addNew || data == 0) {
            _s.addNew = !_s.addNew;
            return false;
        }

        if (!data.functionCode || !data.departmentName) {
            return false;
        }

        _h.post("../OrgUtility/addNewDepartment", {
            data: data,
            tag: 0
        }).then(function (c) {
            _s.Directory.departments = c.data;
            _s.addNew = !_s.addNew;
        });
    }

    //_s.changeSort = function (data, tag) {

    //    _h.post("../OrgUtility/addNewDepartment", {
    //        data: data,
    //        tag: tag
    //    }).then(function (c) {
    //        _s.Directory.departments = c.data;
    //    });

    //}

    _s.activeChange = function (data, tag) {

        if (tag == 3) {
            swal({
                title: "Are you sure you want to remove?",
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "Cancel",
                closeOnConfirm: true
            }, function () {
                _h.post("../OrgUtility/addNewDepartment", {
                    data: data,
                    tag: tag
                }).then(function (c) {
                    _s.Directory.departments = c.data;
                });
            });
        } else {
            _h.post("../OrgUtility/addNewDepartment", {
                data: data,
                tag: tag
            }).then(function (c) {
                _s.Directory.departments = c.data;
            });
        }
        
    }

    _s.editOffice = function (data) {

        data.goEdit = !data.goEdit;
        if (data.goEdit) {
            _s.toedit = angular.copy(data);
        }
        return true;
    }

    _s.selectFromList = function (data) {

        _s.selectedD = data;
        var selected = _s.Directory[data + "s"];
        _s.selectedDirectory = [];

        selected.forEach(function (currentValue) {
            _s.selectedDirectory.push({
                name: currentValue[data + "Name"],
                code: currentValue[data + "Code"],
                canedit: currentValue.canedit
            });
        });        
    }

    _s.numberonly2 = function (data) {
        if (!data) {
            return false;
        }
        _s.toedit.functionCode = data.substring(0, 4);
        return true;
    }

    _s.saveEdit = function (data, tag) {

        if (data == 0 && tag == 0) {
            _s.addNew = !_s.addNew;
            return false;
        }

        if (tag == 2) {
            swal({
                title: "Are you sure you want to remove?",
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "Cancel",
                closeOnConfirm: true
            }, function () {
                data[_s.selectedD + "Name"] = data.name;
                data[_s.selectedD + "Code"] = data.code;
                _h.post("../OrgUtility/addNew" + _s.selectedD, {
                    data: data,
                    tag: tag
                }).then(function (c) {
                    _s.selectedDirectory.splice(_s.selectedDirectory.indexOf(data), 1)
                });
            });
        } else {
            data[_s.selectedD + "Name"] = data.name;
            data[_s.selectedD + "Code"] = data.code;
            _h.post("../OrgUtility/addNew" + _s.selectedD, {
                data: data,
                tag: tag
            }).then(function (c) {
                //console.log(data);
                if (tag == 1) {
                    var edit = _f("filter")(_s.selectedDirectory, { code: data.code })[0];
                    edit.goEdit = !data.goEdit;
                    edit.name = data.name;
                    _s.Directory[_s.selectedD + "s"].splice(_s.selectedDirectory.indexOf(data), 1, data);
                } else {
                    _s.selectedDirectory.push({
                        name: data.name,
                        code: c.data,
                        canedit: true
                    });
                    _s.addNew = !_s.addNew;                    
                }
                
            });
        }

    }

    //_s.editItem = function (data) {
    //    data.goEdit = !data.goEdit;
    //    if (data.goEdit) {
    //        _s.toedit = data;
    //    }
    //    return true;
    //}

}]);
