function Expense(id, Date, Amount, Description, Categories) {
    var self = this;
    self.id = ko.observable(id);
    self.Date = ko.observable(Date);
    self.Amount = ko.observable(Amount);
    self.Description = ko.observable(Description);
    self.BudgetCategories = ko.observableArray(Categories);

    self.EditAmount = ko.computed({
        read: function () {
            return self.Amount();
        },
        write: function (value) {
            value = parseFloat(value);
            self.Amount(value);
        }
    });
}

Expense.prototype.toJSON = function () {
    var copy = ko.toJS(this); //easy way to get a clean copy
    delete copy.EditAmount; //remove an extra property
    return copy; //return the copy to be serialized
};

// Overall viewmodel for this screen, along with initial state
function ExpensesViewModel() {
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

    // Non-editable catalog data - would come from the server
    $.ajax({
        url: '/api/ExpenseAPI',
        success: function (data, status, xhr) {
            for (var i = 0; i < data.length; i++) {
                self.expenses.push(new Expense(data[i].id, new Date(data[i].Date), parseFloat(data[i].Amount), data[i].Description, data[i].BudgetCategories));
            }
        }
    });

    // Editable data
    self.expenses = ko.observableArray([]);

    self.addExpense = function () {
        self.expenses.push(new Expense(null, new Date(), 0, "", []));
    }

    self.removeExpense = function (expense) {
        self.expenses.remove(expense);
        $.ajax({
            type: 'DELETE',
            url: '/api/ExpenseAPI/' + expense.id(),
            contentType: 'application/json; charset=utf-8',
        }).done(function (msg) {
            //console.log("Data deleted: " + msg);
        });
    }

    //self.totalBudget = ko.computed(function () {
    //    var total = 0;
    //    for (var i = 0; i < self.categories().length; i++)
    //        total += parseFloat(self.categories()[i].Value());
    //    return total;
    //});

    self.save = function () {
        for (var i = 0; i < self.expenses().length; i++) {
            (function () {
                var index = i;
                if (self.expenses()[i].id() === null) {
                    var expenseData = ko.toJSON({ 'Date': self.expenses()[i].Date, 'Amount': self.expenses()[i].Amount, 'Description': self.expenses()[i].Description, 'BudgetCategories': self.expenses()[i].BudgetCategories });
                    $.ajax({
                        type: 'POST',
                        url: '/api/ExpenseAPI',
                        contentType: 'application/json; charset=utf-8',
                        data: expenseData,
                    }).done(function (msg) {
                        self.expenses()[index].id(msg.id);
                        //console.log(msg);
                    });
                } else {
                    var expenseData = ko.toJSON(self.expenses()[i]);
                    $.ajax({
                        type: 'PUT',
                        url: '/api/ExpenseAPI/' + self.expenses()[i].id(),
                        contentType: 'application/json; charset=utf-8',
                        data: expenseData,
                    }).done(function (msg) {
                        //console.log(msg);
                    });
                }
            })();
        }
    };
}

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker(options);
        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            current = $(element).datepicker("getDate");

        if (value - current !== 0) {
            $(element).datepicker("setDate", value);
        }
    }
};

ko.bindingHandlers.chosen = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        //initialize datepicker with some optional options
        $(element).chosen();

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            // blah
        });

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        ko.bindingHandlers.options.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

        setTimeout(function () {
            $(element).trigger("liszt:updated");
        }, 0);
    }
};

