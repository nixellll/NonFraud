function GridConfiguration() {
    var datasource = {
        transport: {
            read:
            {
                url: "http://localhost:34566/api/Transaction",
                dataType: "json",
            },
            schema: {
                model: {
                    id: "TransactionID",
                    fields: {
                        TransactionDate: { type: "date" },
                        Type: { type: "string" },
                        Amount: { type: "number" },
                        NameOrig: { type: "string" },
                        OldBalanceOrig: { type: "number" },
                        NewBalanceOrig: { type: "number" },
                        NameDest: { type: "string" },
                        OldBalanceDest: { type: "number" },
                        NewBalanceDest: { type: "number" },
                        IsFraud: { type: "boolean" },
                        IsFlagged: { type: "boolean" }
                    }
                }
            }
        }
    };

    $("#grid").kendoGrid({
        sortable: true,
        resizable: true,
        columns: [
            { field: "TransactionDate", title: "Transaction Date" },
            { field: "Type" },
            { field: "Amount" },
            { field: "NameOrig", title: "Name Origin" },
            { field: "OldBalanceOrig", title: "Old Balance Origin" },
            { field: "NewBalanceOrig", title: "New Balance Origin" },
            { field: "NameDest", title: "Name Recipient" },
            { field: "OldBalanceDest", title: "Old Balance Recipient" },
            { field: "NewBalanceDest", title: "New Balance Recipient" },
            { field: "IsFraud", title: "Is Fraud" },
            { field: "IsFlagged", title: "Is Flagged" }
        ],
        dataSource: datasource
    }).data("kendoGrid");

    $("#isFraudInput").change(function () {
        applyFilter("IsFraud", $(this).prop('checked'));
    });

    $("#searchNameDest").click(function () {
        applyFilter("NameDest", $("#nameDestInput").val());
    });

    $("#searchDate").click(function () {
        if ($("#trxDateInput").val() != "")
            applyFilter("TransactionDate", $("#trxDateInput").val());
    });

    $("#clear").click(function () {
        clearFilters();
    });
}

function applyFilter(filterField, filterValue) {
    var gridData = $("#grid").data("kendoGrid");
    var currFilterObj = gridData.dataSource.filter();
    var currentFilters = currFilterObj ? currFilterObj.filters : [];

    if (currentFilters && currentFilters.length > 0) {
        for (var i = 0; i < currentFilters.length; i++) {
            if (currentFilters[i].field == filterField) {
                currentFilters.splice(i, 1);
                break;
            }
        }
    }

    if (filterField == "IsFraud") {
        currentFilters.push({
            field: filterField,
            operator: "eq",
            value: filterValue
        });
    }
    else {
        currentFilters.push({
            field: filterField,
            operator: "contains",
            value: filterValue
        });
    }

    // finally, the currentFilters array is applied back to the Grid, using "and" logic.
    gridData.dataSource.filter({
        logic: "and",
        filters: currentFilters
    });

}

function clearFilters() {
    var gridData = $("#grid").data("kendoGrid");
    gridData.dataSource.filter({});
}

