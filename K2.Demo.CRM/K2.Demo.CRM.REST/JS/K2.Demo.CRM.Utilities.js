function Querystring(qs) {
    var querystring = "";
    if (qs != null && qs != "") {
        querystring = qs;
    }
    else {
        querystring = location.search.substring(1, location.search.length);
    }

    var args = querystring.split('&');
    for (var i = 0; i < args.length; i++) {
        var pair = args[i].split('=');

        temp = unescape(pair[0]).split('+');

        name = temp.join(' ');
        temp = unescape(pair[1]).split('+');
        value = temp.join(' ');
        this[name] = value;
    }
    this.get = Querystring_get;
}

function Querystring_get(strKey, strDefault) {
    var value = this[strKey];
    if (value == null) {
        value = strDefault;
    }
    return value;
}

function removeGUIDBrackets(g) {
    var u = g;
    u = g.replace("{", "");
    return u.replace("}", "");
}

function getQueryString() {
    var qs = "";
    if (parent.location != null) {
        qs = decodeURIComponent(parent.location.search.substring(1, parent.location.search.length));
    } else {
        qs = decodeURIComponent(location.search.substring(1, location.search.length));
    }

    return qs;
}

function getExtraQS() {
    var qs = "";
    if (parent.location != null) {
        qs = parent.location.search.substring(1, parent.location.search.length);
    } else {
        qs = location.search.substring(1, location.search.length);
    }

    var qs1 = new Querystring(qs);
    var extraqs = removeGUIDBrackets(qs1.get("extraqs", ""));

    return extraqs;
}

function formatJSONDate(value) {
    if (value.slice(1, 6) == 'Date(') {
        var jd = value.slice(6, -2);
        var d = new Date(parseInt(jd));
        return d;
    }
    return "";
}
