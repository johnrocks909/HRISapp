 
var OrgUtility = angular.module('PosUtility', ["smart-table", "ui.select2"]);
OrgUtility.controller('PosUtilityController', ['$scope', '$filter', '$http', function (_s, _f, _h) {

    _s.onLoad = function () {
        _h.get("../OrgUtility/getPosition").then(function (c) {
            _s.Directory = c.data;
        });
    }

    _s.addNewPosition = function (data,tag) {
        if ((tag == 1 && !_s.addNew) || data == 0) {
            _s.addNew = !_s.addNew;
            return false;
        }

        if (!data.positionTitle || !data.salaryGrade) {
            return false;
        }

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
                _h.post("../OrgUtility/addNewPosition", {
                    data: data,
                    tag: tag
                }).then(function (c) {
                    _s.Directory.positions.splice(_s.Directory.positions.indexOf(_f("filter")(_s.Directory.positions, { positionCode: data.positionCode })[0]), 1);
                });
            });
        } else {
            _h.post("../OrgUtility/addNewPosition", {
                data: data,
                tag: tag
            }).then(function (c) {
                if (tag == 1) {
                    data["positionCode"] = c.data;
                    _s.Directory.positions.push(data);
                    _s.addNew = !_s.addNew;
                } else if (tag == 2) {
                    var edit = _f("filter")(_s.Directory.positions, { positionCode: data.positionCode })[0];
                    edit.goEdit = false;
                    edit.positionTitle = data.positionTitle;
                    edit.shortPositionTitle = data.shortPositionTitle;
                    edit.acronym = data.acronym;
                    edit.salaryGrade = data.salaryGrade;
                    edit.positionLevel = data.positionLevel;
                }
            });
        }            
    }

    _s.editPosition = function (data) {
        data.goEdit = !data.goEdit;
        if (data.goEdit) {
            _s.toedit = angular.copy(data);
        }
        return true;
    }

    _s.saveEdit = function (data, tag) {
        if ((tag == 1 && !_s.addNew) || data == 0) {
            _s.addNew = !_s.addNew;
            return false;
        }

        if (!data.magnaCarta) {
            return false;
        }

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
                _h.post("../OrgUtility/addNewMagnacarta", {
                    data: data,
                    tag: tag
                }).then(function (c) {
                    _s.Directory.magnacartas.splice(_s.Directory.magnacartas.indexOf(_f("filter")(_s.Directory.magnacartas, { magnaCode: data.magnaCode })[0]), 1);
                });
            });
        } else {
            _h.post("../OrgUtility/addNewMagnacarta", {
                data: data,
                tag: tag
            }).then(function (c) {
                if (tag == 1) {
                    data["magnaCode"] = c.data;
                    _s.Directory.magnacartas.push(data);
                    _s.addNew = !_s.addNew;
                } else if (tag == 2) {
                    var edit = _f("filter")(_s.Directory.magnacartas, { magnaCode: data.magnaCode })[0];
                    edit.goEdit = false;
                    edit.magnaCarta = data.magnaCarta;
                }
            });
        }
    }

    _s.editMC = function (data) {
        data.goEdit = !data.goEdit;
        if (data.goEdit) {
            _s.toeditm = angular.copy(data);
        }
        return true;
    }

}]);
