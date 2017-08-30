var exclude = ['Id', 'User', 'Img', 'Author'];
var error = {
    validModel: function (scopeModel, scopeErrors, ex) {
        console.log(scopeModel)
        var errArr = [];
        if (ex !== undefined && ex !== null) {
            for (var i = 0; i < ex.length; i++) {
                exclude.push(ex);
            }
        }
        for (var fields in scopeModel) {
            if (scopeModel[fields] === '' && exclude.indexOf(fields) === -1) {
                errArr.push({ [fields]: fields + ' needs to be filled' });
            }
        }
        if (errArr.length > 0) {
            return error.errors(errArr);
        } else {
            scopeErrors = [];
            return scopeErrors;
        }

    },
    errors: function (errorArray, scopeErrors) {
        scopeErrors = [];
        if (errorArray != undefined && errorArray.length > 0) {
            for (var i = 0; i < errorArray.length; i++) {
                scopeErrors.push(errorArray[i]);
            }
        }
        return scopeErrors;
    }
}