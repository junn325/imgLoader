function subdomain_from_url(url, base) {
    var retval = 'b';
    if (base) {
        retval = base;
    }

    var number_of_frontends = 3;
    var b = 16;

    var r = /\/[0-9a-f]\/([0-9a-f]{2})\//;
    var m = r.exec(url);
    if (!m) {
        return 'a';
    }

    var g = parseInt(m[1], b);
    if (!isNaN(g)) {
        if (g < 0x30) {
            number_of_frontends = 2;
        }
        if (g < 0x09) {
            g = 1;
        }
        retval = subdomain_from_galleryid(g, number_of_frontends) + retval;
    }

    return retval;
}

