function Category(id, Name, Value) {
    var self = this;
    self.id = ko.observable(id);
    self.Name = ko.observable(Name);
    self.Value = ko.observable(Value);
    self.Type = ko.observable();

    self.EditValue = ko.computed({
        read: function () {
            return self.Value();
        },
        write: function (value) {
            value = parseFloat(value);
            self.Value(value);
        }
    });
}

Category.prototype.toJSON = function () {
    var copy = ko.toJS(this); //easy way to get a clean copy
    delete copy.EditValue; //remove an extra property
    return copy; //return the copy to be serialized
};

// Overall viewmodel for this screen, along with initial state
function CategoriesViewModel() {
    var self = this;

    // Non-editable catalog data - would come from the server
    $.ajax({
        url: '/api/CategoryAPI',
        success: function (data, status, xhr) {
            for (var i = 0; i < data.length; i++) {
                //console.log(data[i]);
                self.categories.push(new Category(data[i].id, data[i].Name, data[i].Value));
            }
        }
    });

    // Editable data
    self.categories = ko.observableArray([
    ]);

    self.addCategory = function () {
        self.categories.push(new Category(null, "", 0));
    }

    self.removeCategory = function (category) {
        self.categories.remove(category);
        $.ajax({
            type: 'DELETE',
            url: '/api/CategoryAPI/' + category.id(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (msg) {
            //console.log("Data deleted: " + msg);
        });
    }

    self.totalBudget = ko.computed(function () {
        var total = 0;
        for (var i = 0; i < self.categories().length; i++)
            total += parseFloat(self.categories()[i].Value());
        return total;
    });

    self.save = function () {
        for (var i = 0; i < self.categories().length; i++) {
            (function () {
                var index = i;
                if (self.categories()[i].id() === null) {
                    var categoryData = ko.toJSON({ 'Name': self.categories()[i].Name, 'Value': self.categories()[i].Value });
                    $.ajax({
                        type: 'POST',
                        url: '/api/CategoryAPI',
                        contentType: 'application/json; charset=utf-8',
                        data: categoryData,
                    }).done(function (msg) {
                        self.categories()[index].id(msg.id);
                        //console.log(msg);
                    });
                } else {
                    var categoryData = ko.toJSON(self.categories()[i]);
                    $.ajax({
                        type: 'PUT',
                        url: '/api/CategoryAPI/' + self.categories()[i].id(),
                        contentType: 'application/json; charset=utf-8',
                        data: categoryData,
                    }).done(function (msg) {
                        //console.log(msg);
                    });
                }
            })();
        }
    };
}

$(document).ready(function () {
    ko.applyBindings(new CategoriesViewModel());
});