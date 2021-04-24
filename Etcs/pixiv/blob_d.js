var CONFETTI, SIZE = {}, module = {};
(function e(t, n, i, o) {
    var a = !!(t.Worker && t.Blob && t.Promise && t.OffscreenCanvas && t.HTMLCanvasElement && t.HTMLCanvasElement.prototype.transferControlToOffscreen && t.URL && t.URL.createObjectURL)
    function s() { } function u(e) {
        var r = n.exports.Promise, i = void 0 !== r ? r : t.Promise
        return "function" == typeof i ? new i(e) : (e(s, s), null)
    } var c, l, f, d, p, h, v, g, m = (f = Math.floor(1e3 / 60), d = {}, p = 0, "function" == typeof requestAnimationFrame && "function" == typeof cancelAnimationFrame ? (c = function (e) {
        var t = Math.random()
        return d[t] = requestAnimationFrame((function n(r) { p === r || p + f - 1 < r ? (p = r, delete d[t], e()) : d[t] = requestAnimationFrame(n) })), t
    }, l = function (e) { d[e] && cancelAnimationFrame(d[e]) }) : (c = function (e) { return setTimeout(e, f) }, l = function (e) { return clearTimeout(e) }), { frame: c, cancel: l }), y = (g = {}, function () {
        if (h) return h
        if (!i && a) {
            var t = ["var CONFETTI, SIZE = {}, module = {};", "(" + e.toString() + ")(this, module, true, SIZE);", "onmessage = function(msg) {", "  if (msg.data.options) {", "    CONFETTI(msg.data.options).then(function () {", "      if (msg.data.callback) {", "        postMessage({ callback: msg.data.callback });", "      }", "    });", "  } else if (msg.data.reset) {", "    CONFETTI.reset();", "  } else if (msg.data.resize) {", "    SIZE.width = msg.data.resize.width;", "    SIZE.height = msg.data.resize.height;", "  } else if (msg.data.canvas) {", "    SIZE.width = msg.data.canvas.width;", "    SIZE.height = msg.data.canvas.height;", "    CONFETTI = module.exports.create(msg.data.canvas);", "  }", "}"].join("\n")
            try { h = new Worker(URL.createObjectURL(new Blob([t]))) } catch (n) { return void 0 !== ("undefined" == typeof console ? "undefined" : Object(r.a)(console)) && "function" == typeof console.warn && console.warn("�럧 Count not load worker", n), null } !function (e) {
                function t(t, n) { e.postMessage({ options: t || {}, callback: n }) } e.init = function (t) {
                    var n = t.transferControlToOffscreen()
                    e.postMessage({ canvas: n }, [n])
                }, e.fire = function (n, r, i) {
                    if (v) return t(n, null), v
                    var o = Math.random().toString(36).slice(2)
                    return v = u((function (r) { function a(t) { t.data.callback === o && (delete g[o], e.removeEventListener("message", a), v = null, i(), r()) } e.addEventListener("message", a), t(n, o), g[o] = a.bind(null, { data: { callback: o } }) }))
                }, e.reset = function () { for (var t in e.postMessage({ reset: !0 }), g) g[t](), delete g[t] }
            }(h)
        } return h
    }), b = { particleCount: 50, angle: 90, spread: 45, startVelocity: 45, decay: .9, gravity: 1, ticks: 200, x: .5, y: .5, shapes: ["square", "circle"], zIndex: 100, colors: ["#26ccff", "#a25afd", "#ff5e7e", "#88ff5a", "#fcff42", "#ffa62d", "#ff36ff"], disableForReducedMotion: !1 }
    function w(e, t, n) { return function (e, t) { return t ? t(e) : e }(e && null != e[t] ? e[t] : b[t], n) } function O(e) { return parseInt(e, 16) } function x(e) { e.width = document.documentElement.clientWidth, e.height = document.documentElement.clientHeight } function k(e) {
        var t = e.getBoundingClientRect()
        e.width = t.width, e.height = t.height
    } function j(e, t, n, r, a) {
        var s, c, l = t.slice(), f = e.getContext("2d"), d = u((function (t) {
            function u() { s = c = null, f.clearRect(0, 0, r.width, r.height), a(), t() } s = m.frame((function t() {
                !i || r.width === o.width && r.height === o.height || (r.width = e.width = o.width, r.height = e.height = o.height), r.width || r.height || (n(e), r.width = e.width, r.height = e.height), f.clearRect(0, 0, r.width, r.height), (l = l.filter((function (e) {
                    return function (e, t) {
                        t.x += Math.cos(t.angle2D) * t.velocity, t.y += Math.sin(t.angle2D) * t.velocity + t.gravity, t.wobble += .1, t.velocity *= t.decay, t.tiltAngle += .1, t.tiltSin = Math.sin(t.tiltAngle), t.tiltCos = Math.cos(t.tiltAngle), t.random = Math.random() + 5, t.wobbleX = t.x + 10 * Math.cos(t.wobble), t.wobbleY = t.y + 10 * Math.sin(t.wobble)
                        var n = t.tick++ / t.totalTicks, r = t.x + t.random * t.tiltCos, i = t.y + t.random * t.tiltSin, o = t.wobbleX + t.random * t.tiltCos, a = t.wobbleY + t.random * t.tiltSin
                        return e.fillStyle = "rgba(" + t.color.r + ", " + t.color.g + ", " + t.color.b + ", " + (1 - n) + ")", e.beginPath(), "circle" === t.shape ? e.ellipse ? e.ellipse(t.x, t.y, Math.abs(o - r) * t.ovalScalar, Math.abs(a - i) * t.ovalScalar, Math.PI / 10 * t.wobble, 0, 2 * Math.PI) : function (e, t, n, r, i, o, a, s, u) { e.save(), e.translate(t, n), e.rotate(o), e.scale(r, i), e.arc(0, 0, 1, a, s, u), e.restore() }(e, t.x, t.y, Math.abs(o - r) * t.ovalScalar, Math.abs(a - i) * t.ovalScalar, Math.PI / 10 * t.wobble, 0, 2 * Math.PI) : (e.moveTo(Math.floor(t.x), Math.floor(t.y)), e.lineTo(Math.floor(t.wobbleX), Math.floor(i)), e.lineTo(Math.floor(o), Math.floor(a)), e.lineTo(Math.floor(r), Math.floor(t.wobbleY))), e.closePath(), e.fill(), t.tick < t.totalTicks
                    }(f, e)
                }))).length ? s = m.frame(t) : u()
            })), c = u
        }))
        return { addFettis: function (e) { return l = l.concat(e), d }, canvas: e, promise: d, reset: function () { s && m.cancel(s), c && c() } }
    } function S(e, n) {
        var r, i = !e, o = !!w(n || {}, "resize"), s = w(n, "disableForReducedMotion", Boolean), c = a && !!w(n || {}, "useWorker") ? y() : null, l = i ? x : k, f = !(!e || !c) && !!e.__confetti_initialized, d = "function" == typeof matchMedia && matchMedia("(prefers-reduced-motion)").matches
        function p(t, n, i) {
            for (var o, a, s, u, c, f, d, p = w(t, "particleCount", Math.floor), h = w(t, "angle", Number), v = w(t, "spread", Number), g = w(t, "startVelocity", Number), m = w(t, "decay", Number), y = w(t, "gravity", Number), b = w(t, "colors"), x = w(t, "ticks", Number), k = w(t, "shapes"), S = function (e) {
                var t = w(e, "origin", Object)
                return t.x = w(t, "x", Number), t.y = w(t, "y", Number), t
            }(t), _ = p, E = [], A = e.width * S.x, T = e.height * S.y; _--;)E.push((o = { x: A, y: T, angle: h, spread: v, startVelocity: g, color: b[_ % b.length], shape: k[(f = 0, d = k.length, Math.floor(Math.random() * (d - f)) + f)], ticks: x, decay: m, gravity: y }, a = void 0, s = void 0, u = void 0, c = void 0, u = o.angle * (Math.PI / 180), c = o.spread * (Math.PI / 180), { x: o.x, y: o.y, wobble: 10 * Math.random(), velocity: .5 * o.startVelocity + Math.random() * o.startVelocity, angle2D: -u + (.5 * c - Math.random() * c), tiltAngle: Math.random() * Math.PI, color: (a = o.color, s = String(a).replace(/[^0-9a-f]/gi, ""), s.length < 6 && (s = s[0] + s[0] + s[1] + s[1] + s[2] + s[2]), { r: O(s.substring(0, 2)), g: O(s.substring(2, 4)), b: O(s.substring(4, 6)) }), shape: o.shape, tick: 0, totalTicks: o.ticks, decay: o.decay, random: Math.random() + 5, tiltSin: 0, tiltCos: 0, wobbleX: 0, wobbleY: 0, gravity: 3 * o.gravity, ovalScalar: .6 }))
            return r ? r.addFettis(E) : (r = j(e, E, l, n, i)).promise
        } function h(n) {
            var a = s || w(n, "disableForReducedMotion", Boolean), h = w(n, "zIndex", Number)
            if (a && d) return u((function (e) { e() }))
            i && r ? e = r.canvas : i && !e && (e = function (e) {
                var t = document.createElement("canvas")
                return t.style.position = "fixed", t.style.top = "0px", t.style.left = "0px", t.style.pointerEvents = "none", t.style.zIndex = e, t
            }(h), document.body.appendChild(e)), o && !f && l(e)
            var v = { width: e.width, height: e.height }
            function g() {
                if (c) {
                    var t = { getBoundingClientRect: function () { if (!i) return e.getBoundingClientRect() } }
                    return l(t), void c.postMessage({ resize: { width: t.width, height: t.height } })
                } v.width = v.height = null
            } function m() { r = null, o && t.removeEventListener("resize", g), i && e && (document.body.removeChild(e), e = null, f = !1) } return c && !f && c.init(e), f = !0, c && (e.__confetti_initialized = !0), o && t.addEventListener("resize", g, !1), c ? c.fire(n, v, m) : p(n, v, m)
        } return h.reset = function () { c && c.reset(), r && r.reset() }, h
    } n.exports = S(null, { useWorker: !0, resize: !0 }), n.exports.create = S
})(this, module, true, SIZE);
onmessage = function (msg) {
    if (msg.data.options) {
        CONFETTI(msg.data.options).then(function () {
            if (msg.data.callback) {
                postMessage({ callback: msg.data.callback });
            }
        });
    } else if (msg.data.reset) {
        CONFETTI.reset();
    } else if (msg.data.resize) {
        SIZE.width = msg.data.resize.width;
        SIZE.height = msg.data.resize.height;
    } else if (msg.data.canvas) {
        SIZE.width = msg.data.canvas.width;
        SIZE.height = msg.data.canvas.height;
        CONFETTI = module.exports.create(msg.data.canvas);
    }
}