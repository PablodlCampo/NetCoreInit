function setSort(element, sort) {

    if (element.attr("sort") == "") {
        element.attr("sort", "asc");
        sort = sort + "_asc";
    }
    else {
        if (element.attr("sort") == "asc") {
            element.attr("sort", "desc")
            sort = sort + "_desc";
        }
        else {
            element.attr("sort", "asc")
            sort = sort + "_asc";
        }
    }
    return sort;
}