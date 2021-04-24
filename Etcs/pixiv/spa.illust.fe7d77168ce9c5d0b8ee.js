(window.webpackJsonp = window.webpackJsonp || []).push([
  [59, 101],
  {
    1055: function (t, e, n) {
      "use strict";
      n.r(e);
      n(28), n(53), n(164), n(176);
      var i = n(1),
        r = n(8),
        o = n(34),
        c = n(37),
        a = n(24),
        s = n(36),
        l = n(45),
        u = n(30),
        d = n(4),
        b = n(0),
        h = n(6),
        f = n.n(h),
        j = n(41),
        p = n(2),
        O = n(5),
        m = n(87),
        v = n(226),
        g = n(1314),
        x = n(400),
        y = n(11);
      function w(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(u.a)(t);
          if (e) {
            var r = Object(u.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(l.a)(this, n);
        };
      }
      function k(t) {
        var e = t.labelType,
          n = Object(O.p)([O.m.action.ns, O.m.func.ns, O.m.header.ns]),
          i = Object(r.a)(n, 1)[0],
          o = Object(O.o)(),
          c = Object(r.a)(o, 1)[0];
        return Object(b.jsxs)(I, {
          children: [
            Object(b.jsx)(_, {
              children:
                e === v.a.Follow
                  ? c("�╉꺖�뜰꺖�믡깢�⒲꺆�쇈걲��")
                  : e === v.a.Bookmark
                  ? c("鵝쒎뱚��%(heart)�쇻굥", {
                      heart: Object(b.jsx)(x.c, { size: 22, active: !0 }),
                    })
                  : e === v.a.Like
                  ? c("鵝쒎뱚�ャ걚�꾠겖竊곥걲��")
                  : e === v.a.Post
                  ? i(O.m.action.link.upload_post)
                  : e === v.a.SendMessage
                  ? c("�▲긿�삠꺖�멥굮�곥굥")
                  : e === v.a.BookmarkPage
                  ? c("�뽧긿��깯�쇈궚�믦쫳��")
                  : e === v.a.FollowPage
                  ? c("�뺛궔��꺖訝�誤㎯굮誤뗣굥")
                  : e === v.a.EnlargeWorks
                  ? i(O.m.func.work_viewer.display_original_size)
                  : e === v.a.SearchUser
                  ? i(O.m.header.yu_za_woken)
                  : e === v.a.SearchOptions
                  ? i(O.m.func.search.search_option_label)
                  : e === v.a.OrderDateAsc
                  ? i(O.m.func.search.sort_oldest_to_newest)
                  : e === v.a.FavoriteTag
                  ? i(O.m.header.okiniiritag.okiniirinitsuika)
                  : Object(y.v)(e),
            }),
            Object(b.jsx)(S, {
              children: c(
                "pixiv�㏂궖�╉꺍�덀굮鵝쒏닇�쇻굥�ⓧ슴�덀굥�덀걝�ャ겒�듽겲�쇻��"
              ),
            }),
          ],
        });
      }
      e.default = Object(j.i)(
        (function (t) {
          Object(s.a)(n, t);
          var e = w(n);
          function n() {
            var t;
            Object(o.a)(this, n);
            for (var i = arguments.length, r = new Array(i), c = 0; c < i; c++)
              r[c] = arguments[c];
            return (
              (t = e.call.apply(e, [this].concat(r))),
              Object(d.a)(Object(a.a)(t), "state", { returnTo: "" }),
              t
            );
          }
          return (
            Object(c.a)(n, [
              {
                key: "componentDidUpdate",
                value: function (t) {
                  ((!1 === t.show && !0 === this.props.show) ||
                    t.location !== this.props.location) &&
                    this.setState({ returnTo: location.href });
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this.props,
                    e = t.show,
                    n = t.onClose,
                    i = t.labelType,
                    r = t.ref,
                    o =
                      void 0 !== this.props.returnTo
                        ? this.props.returnTo
                        : this.state.returnTo;
                  return Object(b.jsxs)(m.e, {
                    show: e,
                    width: 336,
                    onBackgroundClick: n,
                    isForm: !1,
                    children: [
                      Object(b.jsx)(m.d, { onCloseClick: n }),
                      Object(b.jsx)(m.a, {
                        children: Object(b.jsxs)(C, {
                          children: [
                            Object(b.jsx)(k, { labelType: i }),
                            Object(b.jsx)(g.a, { ref: r, returnTo: o }),
                          ],
                        }),
                      }),
                    ],
                  });
                },
              },
            ]),
            n
          );
        })(f.a.Component)
      );
      var C = p.default.div.withConfig({ componentId: "dh7p4n-0" })([
          "padding-bottom:40px;text-align:center;",
        ]),
        I = p.default.div.withConfig({ componentId: "dh7p4n-1" })([
          "margin:4px 0 36px;",
        ]),
        _ = p.default.div.withConfig({ componentId: "dh7p4n-2" })([
          "display:flex;align-items:center;justify-content:center;font-size:16px;font-weight:bold;line-height:24px;margin-bottom:32px;padding:0 16px;",
        ]),
        S = p.default.div.withConfig({ componentId: "dh7p4n-3" })(
          ["font-size:14px;line-height:22px;color:", ";padding:0 16px;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.note;
          }.bind(void 0)
        );
    },
    1282: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return g;
      }),
        n.d(e, "c", function () {
          return x;
        }),
        n.d(e, "b", function () {
          return w;
        });
      n(28), n(54), n(29), n(81);
      var i = n(1),
        r = n(34),
        o = n(37),
        c = n(24),
        a = n(36),
        s = n(45),
        l = n(30),
        u = n(4),
        d = n(0),
        b = n(6),
        h = n.n(b),
        f = n(355),
        j = n(2);
      function p(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function O(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? p(Object(n), !0).forEach(function (e) {
                Object(u.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : p(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function m(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(l.a)(t);
          if (e) {
            var r = Object(l.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(s.a)(this, n);
        };
      }
      var v = "$$Separator$".concat(Math.random().toString(36).slice(2), "$"),
        g = { label: v, value: void 0, isDisabled: !0 },
        x = (function (t) {
          Object(a.a)(n, t);
          var e = m(n);
          function n() {
            var t,
              o = this;
            Object(r.a)(this, n);
            for (var a = arguments.length, s = new Array(a), l = 0; l < a; l++)
              s[l] = arguments[l];
            return (
              (t = e.call.apply(e, [this].concat(s))),
              Object(u.a)(
                Object(c.a)(t),
                "blockEvent",
                function (t) {
                  Object(i.a)(this, o),
                    t.preventDefault(),
                    t.stopPropagation(),
                    y(t.target) &&
                      (t.target.target
                        ? window.open(t.target.href, t.target.target)
                        : (window.location.href = t.target.href));
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(o.a)(n, [
              {
                key: "render",
                value: function () {
                  if (this.props.label === v) return Object(d.jsx)(C, {});
                  var t = this.props,
                    e = t.className,
                    n = t.isDisabled,
                    i = t.isFocused,
                    r = t.isSelected,
                    o = t.innerProps,
                    c = t.data.isPremium;
                  return Object(d.jsx)(
                    I,
                    O(
                      O({}, o),
                      {},
                      {
                        className: e,
                        role: n ? void 0 : "option",
                        onClick: n
                          ? this.blockEvent
                          : this.props.innerProps.onClick,
                        isDisabled: n,
                        isFocused: i,
                        isSelected: r,
                        children: n
                          ? this.props.children
                          : Object(d.jsxs)(d.Fragment, {
                              children: [
                                r && Object(d.jsx)(w, {}),
                                Object(d.jsx)("span", {
                                  children: this.props.children,
                                }),
                                c && Object(d.jsx)(f.a, {}),
                              ],
                            }),
                      }
                    )
                  );
                },
              },
            ]),
            n
          );
        })(h.a.PureComponent);
      function y(t) {
        return "href" in t && "A" === t.tagName;
      }
      var w = Object(j.default)(k).withConfig({ componentId: "sc-1ino2uq-0" })([
        "margin-top:auto;margin-bottom:auto;flex:none;pointer-events:none;stroke:#000;fill:none;stroke-opacity:0.6;stroke-linecap:round;",
      ]);
      function k(t) {
        return Object(d.jsx)("svg", {
          className: t.className,
          viewBox: "-1 -1 20 16",
          width: 12,
          height: 10,
          children: Object(d.jsx)("polyline", {
            points: "1 7 6 12 17 1",
            strokeWidth: 3,
          }),
        });
      }
      var C = j.default.hr.withConfig({ componentId: "sc-1ino2uq-1" })([
          "border:none;padding:none;margin:7px 9px;height:1px;background:#eee;",
        ]),
        I = j.default.div.withConfig({ componentId: "sc-1ino2uq-2" })(
          [
            "display:flex;cursor:pointer;padding:9px 24px;color:rgba(0,0,0,0.64);font-size:14px;line-height:22px;",
            " ",
            " ",
            "{margin-left:",
            "px;margin-right:4px;}",
          ],
          function (t) {
            return (
              Object(i.a)(this, void 0),
              t.isFocused &&
                Object(j.css)(["background-color:rgba(0,0,0,0.04);"])
            );
          }.bind(void 0),
          function (t) {
            return (
              Object(i.a)(this, void 0),
              t.isDisabled && Object(j.css)(["opacity:0.4;cursor:default;"])
            );
          }.bind(void 0),
          w,
          -16
        );
    },
    1283: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return a;
      });
      var i = n(1),
        r = n(0),
        o = (n(6), n(2)),
        c = n(321);
      function a(t) {
        var e = t.children,
          n = t.title,
          i = t.rightColumn,
          o = t.large,
          a = void 0 !== o && o,
          h = t.warning,
          f = void 0 !== h && h,
          j = t.verticallyCentered,
          p = void 0 !== j && j,
          O = t.showIcon,
          m = void 0 === O || O,
          v = t.iconLineHeight,
          g = void 0 === v ? 15 : v,
          x = t.iconSize,
          y = void 0 === x ? 15 : x;
        return Object(r.jsxs)(s, {
          warning: f,
          large: a,
          children: [
            i && Object(r.jsx)(u, { children: i }),
            void 0 !== n &&
              Object(r.jsxs)(l, {
                children: [
                  m &&
                    Object(r.jsx)(b, {
                      verticallyCentered: p,
                      iconLineHeight: g,
                      iconSize: y,
                      children: Object(r.jsx)(c.b, { size: y }),
                    }),
                  Object(r.jsx)("span", { children: n }),
                ],
              }),
            void 0 === n && m
              ? Object(r.jsxs)(d, {
                  children: [
                    Object(r.jsx)(b, {
                      verticallyCentered: p,
                      iconLineHeight: g,
                      iconSize: y,
                      children: Object(r.jsx)(c.b, { size: y }),
                    }),
                    Object(r.jsx)("div", { children: e }),
                  ],
                })
              : e,
          ],
        });
      }
      var s = o.default.aside.withConfig({ componentId: "kf5fvs-0" })(
          [
            "padding:",
            ";margin:",
            ";max-width:",
            ";border-radius:8px;background:",
            ";color:",
            ";line-height:1.25;",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.large ? "24px" : "16px";
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.large ? "24px auto" : "8px 0";
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.large ? "740px" : "none";
          }.bind(void 0),
          function (t) {
            Object(i.a)(this, void 0);
            var e = t.warning,
              n = t.theme;
            return e ? "#ffe7e4" : n.colors.surface3;
          }.bind(void 0),
          function (t) {
            Object(i.a)(this, void 0);
            var e = t.warning,
              n = t.theme;
            return e ? "#ff2b00" : n.colors.text2;
          }.bind(void 0)
        ),
        l = o.default.h2.withConfig({ componentId: "kf5fvs-1" })([
          "display:flex;align-items:center;color:#666;font-size:12px;font-weight:bold;margin-bottom:8px;line-height:1;",
        ]),
        u = o.default.div.withConfig({ componentId: "kf5fvs-2" })([
          "float:right;",
        ]),
        d = o.default.div.withConfig({ componentId: "kf5fvs-3" })([
          "display:flex;& > div{flex:1 0;}",
        ]),
        b = o.default.span.withConfig({ componentId: "kf5fvs-4" })(
          [
            "flex:none;align-self:flex-start;width:",
            "px;height:",
            "px;margin:",
            ";padding-top:",
            "px;padding-bottom:",
            "px;",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.iconSize;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.iconLineHeight;
          }.bind(void 0),
          function (t) {
            return (
              Object(i.a)(this, void 0),
              t.verticallyCentered ? "auto 4px auto 0" : "0 4px 0 0"
            );
          }.bind(void 0),
          function (t) {
            Object(i.a)(this, void 0);
            var e = t.iconLineHeight,
              n = t.iconSize;
            return Math.max(e - n, 0) / 2;
          }.bind(void 0),
          function (t) {
            Object(i.a)(this, void 0);
            var e = t.iconLineHeight,
              n = t.iconSize;
            return Math.max(e - n, 0) / 2;
          }.bind(void 0)
        );
    },
    1289: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return h;
      });
      n(31), n(362);
      var i = n(1),
        r = n(23),
        o = n(0),
        c = (n(6), n(38)),
        a = n(480),
        s = n(1313),
        l = n(662),
        u = n(14),
        d = n(2),
        b = n(5);
      function h(t) {
        var e = this,
          n = t.request,
          c = Object(l.f)(n, b.m.commission.request_post_status_label);
        return Object(o.jsxs)(f, {
          to: u.commission.requestsDetails({ requestId: n.request.requestId }),
          spa: !0,
          children: [
            Object(o.jsxs)(j, {
              children: [
                Object(r.a)(n.collaborateStatus.userSamples)
                  .reverse()
                  .map(
                    function (t, n) {
                      return (
                        Object(i.a)(this, e),
                        Object(o.jsx)(
                          p,
                          {
                            children:
                              null === t
                                ? Object(o.jsx)(s.a, { size: 24 })
                                : Object(o.jsx)(a.a, {
                                    url: t.profileImg,
                                    userName: t.userName,
                                    size: 24,
                                  }),
                          },
                          n
                        )
                      );
                    }.bind(this)
                  ),
                Object(o.jsx)(p, {
                  children:
                    null === n.fan
                      ? Object(o.jsx)(s.a, { size: 24 })
                      : Object(o.jsx)(a.a, {
                          url: n.fan.profileImg,
                          size: 24,
                          userName: n.fan.userName,
                        }),
                }),
              ],
            }),
            Object(o.jsx)(O, { children: c }),
          ],
        });
      }
      var f = Object(d.default)(c.a).withConfig({
          componentId: "sc-1v0c0el-0",
        })(["display:flex;align-items:center;"]),
        j = d.default.div.withConfig({ componentId: "sc-1v0c0el-1" })([
          "display:inline-flex;flex-flow:row-reverse nowrap;",
        ]),
        p = d.default.div.withConfig({ componentId: "sc-1v0c0el-2" })([
          "margin-right:-8px;",
        ]),
        O = d.default.div.withConfig({ componentId: "sc-1v0c0el-3" })(
          ["margin-left:12px;font-size:14px;line-height:22px;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.colors.text3;
          }.bind(void 0)
        );
    },
    1290: function (t, e, n) {
      "use strict";
      n.d(e, "b", function () {
        return h;
      }),
        n.d(e, "a", function () {
          return f;
        }),
        n.d(e, "c", function () {
          return j;
        });
      var i = n(32),
        r = (n(28), n(53), n(36)),
        o = (n(45), n(30), n(126)),
        c = n(240),
        a = n(0),
        s = (n(6), n(590)),
        l = n(173);
      function u(t, e) {
        u = function (t, e) {
          return new s(t, void 0, e);
        };
        var n = Object(o.a)(RegExp),
          c = RegExp.prototype,
          a = new WeakMap();
        function s(t, e, i) {
          var r = n.call(this, t, e);
          return a.set(r, i || a.get(t)), r;
        }
        function l(t, e) {
          var n = a.get(e);
          return Object.keys(n).reduce(function (e, i) {
            return (e[i] = t[n[i]]), e;
          }, Object.create(null));
        }
        return (
          Object(r.a)(s, n),
          (s.prototype.exec = function (t) {
            var e = c.exec.call(this, t);
            return e && (e.groups = l(e, this)), e;
          }),
          (s.prototype[Symbol.replace] = function (t, e) {
            if ("string" == typeof e) {
              var n = a.get(this);
              return c[Symbol.replace].call(
                this,
                t,
                e.replace(/\$<([^>]+)>/g, function (t, e) {
                  return "$" + n[e];
                })
              );
            }
            if ("function" == typeof e) {
              var r = this;
              return c[Symbol.replace].call(this, t, function () {
                var t = [];
                return (
                  t.push.apply(t, arguments),
                  "object" !== Object(i.a)(t[t.length - 1]) && t.push(l(t, r)),
                  e.apply(this, t)
                );
              });
            }
            return c[Symbol.replace].call(this, t, e);
          }),
          u.apply(this, arguments)
        );
      }
      function d() {
        var t = Object(c.a)([
          "\n  font-size: 1.5em;\n  font-weight: bold;\n  margin: 1em 0;\n",
        ]);
        return (
          (d = function () {
            return t;
          }),
          t
        );
      }
      var b = s.e.h2(d());
      function h(t) {
        return Object(a.jsx)(b, { id: f(t), children: t.children });
      }
      function f(t) {
        return "chapter_"
          .concat(t.pageNumber - 1, "_")
          .concat(t.chapterNumber - 1);
      }
      function j(t) {
        var e = Object(l.a)(
          u(/^#?chapter_([0-9]+)_([0-9]+)$/, { page: 1, chapter: 2 }).exec(t)
        );
        if (null !== e) {
          var n = parseInt(e.page, 10);
          return n >= 0 ? n + 1 : void 0;
        }
      }
    },
    1296: function (t, e, n) {
      "use strict";
      n.d(e, "f", function () {
        return o;
      }),
        n.d(e, "e", function () {
          return c;
        }),
        n.d(e, "c", function () {
          return a;
        }),
        n.d(e, "b", function () {
          return s;
        }),
        n.d(e, "d", function () {
          return l;
        }),
        n.d(e, "a", function () {
          return u;
        }),
        n.d(e, "g", function () {
          return d;
        }),
        n.d(e, "h", function () {
          return b;
        }),
        n.d(e, "k", function () {
          return h;
        }),
        n.d(e, "l", function () {
          return f;
        }),
        n.d(e, "i", function () {
          return j;
        }),
        n.d(e, "j", function () {
          return p;
        });
      var i = n(46),
        r = n.n(i);
      function o(t, e) {
        var n =
            arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : 0,
          i =
            arguments.length > 3 && void 0 !== arguments[3] ? arguments[3] : 5;
        return t.get(
          "/ajax/illusts/comments/roots",
          {},
          { illust_id: "".concat(e), offset: "".concat(n), limit: "".concat(i) }
        );
      }
      function c(t, e) {
        var n =
          arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : 1;
        return t.get(
          "/ajax/illusts/comments/replies",
          {},
          { comment_id: "".concat(e), page: "".concat(n) }
        );
      }
      function a(t, e) {
        var n =
            arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : 0,
          i =
            arguments.length > 3 && void 0 !== arguments[3] ? arguments[3] : 5;
        return t.get(
          "/ajax/novels/comments/roots",
          {},
          { novel_id: "".concat(e), offset: "".concat(n), limit: "".concat(i) }
        );
      }
      function s(t, e) {
        var n =
          arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : 1;
        return t.get(
          "/ajax/novels/comments/replies",
          {},
          { comment_id: "".concat(e), page: "".concat(n) }
        );
      }
      function l(t, e) {
        return t.get(
          "/ajax/illusts/comments/reply_and_root",
          {},
          { comment_id: "".concat(e) }
        );
      }
      function u(t, e) {
        return t.get(
          "/ajax/novels/comments/reply_and_root",
          {},
          { comment_id: "".concat(e) }
        );
      }
      function d(t, e, n, i) {
        return t.post(
          "/rpc/post_comment.php",
          {},
          r.a.stringify({
            type: "comment",
            illust_id: "".concat(e),
            author_user_id: "".concat(n),
            comment: i,
          })
        );
      }
      function b(t, e, n, i) {
        return t.post(
          "/novel/rpc/post_comment.php",
          {},
          r.a.stringify({
            type: "comment",
            novel_id: "".concat(e),
            author_user_id: "".concat(n),
            comment: i,
          })
        );
      }
      function h(t, e, n, i, o) {
        return t.post(
          "/rpc/post_comment.php",
          {},
          r.a.stringify({
            type: "stamp",
            illust_id: "".concat(e),
            author_user_id: "".concat(n),
            stamp_id: "".concat(i),
            parent_id: void 0 !== o ? "".concat(o) : void 0,
          })
        );
      }
      function f(t, e, n, i, o) {
        return t.post(
          "/novel/rpc/post_comment.php",
          {},
          r.a.stringify({
            type: "stamp",
            novel_id: "".concat(e),
            author_user_id: "".concat(n),
            stamp_id: "".concat(i),
            parent_id: void 0 !== o ? "".concat(o) : void 0,
          })
        );
      }
      function j(t, e, n, i, o) {
        return t.post(
          "/rpc/post_comment.php",
          {},
          r.a.stringify({
            type: "comment",
            illust_id: "".concat(e),
            author_user_id: "".concat(n),
            comment: i,
            parent_id: "".concat(o),
          })
        );
      }
      function p(t, e, n, i, o) {
        return t.post(
          "/novel/rpc/post_comment.php",
          {},
          r.a.stringify({
            type: "comment",
            novel_id: "".concat(e),
            author_user_id: "".concat(n),
            comment: i,
            parent_id: "".concat(o),
          })
        );
      }
    },
    1298: function (t, e, n) {
      "use strict";
      n.d(e, "d", function () {
        return c;
      }),
        n.d(e, "a", function () {
          return a;
        }),
        n.d(e, "b", function () {
          return s;
        }),
        n.d(e, "c", function () {
          return l;
        });
      n(326);
      var i = n(1),
        r = n(174),
        o = n(2),
        c = o.default.div.withConfig({ componentId: "nsy2yz-0" })(
          ["", " @media ", "{", "}text-align:center;"],
          Object(r.a)(
            function (t) {
              return (
                Object(i.a)(this, void 0),
                [
                  t.typography(16).bold,
                  t.font.text1,
                  t.padding.top(64).bottom(16),
                ]
              );
            }.bind(void 0)
          ),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.breakpoint.screen1;
          }.bind(void 0),
          Object(r.a)(
            function (t) {
              return Object(i.a)(this, void 0), t.padding.horizontal(16);
            }.bind(void 0)
          )
        ),
        a = o.default.div.withConfig({ componentId: "nsy2yz-1" })(
          ["", " @media ", "{", "}"],
          Object(r.a)(
            function (t) {
              return Object(i.a)(this, void 0), t.padding.horizontal(40);
            }.bind(void 0)
          ),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.breakpoint.screen1;
          }.bind(void 0),
          Object(r.a)(
            function (t) {
              return Object(i.a)(this, void 0), t.padding.horizontal(16);
            }.bind(void 0)
          )
        ),
        s = o.default.div.withConfig({ componentId: "nsy2yz-2" })(
          ["", " text-align:center;"],
          Object(r.a)(
            function (t) {
              return (
                Object(i.a)(this, void 0),
                [t.typography(14), t.font.text2, t.margin.vertical(24)]
              );
            }.bind(void 0)
          )
        ),
        l =
          (Object(r.a)(
            function (t) {
              return Object(i.a)(this, void 0), [t.margin.vertical(24)];
            }.bind(void 0)
          ),
          o.default.div.withConfig({ componentId: "nsy2yz-4" })(
            ["text-align:center;", " & > *{margin:0;}"],
            Object(r.a)(
              function (t) {
                return (
                  Object(i.a)(this, void 0),
                  [t.typography(14), t.font.text2, t.margin.top(40)]
                );
              }.bind(void 0)
            )
          ));
    },
    1302: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return r;
      });
      n(28);
      var i = n(0);
      function r(t) {
        var e = t.gtmData,
          n = t.children,
          r = t.disabled;
        return void 0 !== r && r
          ? Object(i.jsx)(i.Fragment, { children: n })
          : Object(i.jsx)("div", {
              className: "gtm-".concat(e.className, "-zone"),
              "data-gtm-recommend-zone": e.key
                ? "".concat(e.recommendZone, "-").concat(e.key)
                : e.recommendZone,
              children: n,
            });
      }
    },
    1303: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return c;
      });
      var i = n(1),
        r = n(174),
        o = n(2),
        c = o.default.li.withConfig({ componentId: "e1y7e-0" })(
          ["", " list-style-type:disc;display:list-item;"],
          Object(r.a)(
            function (t) {
              return (
                Object(i.a)(this, void 0),
                [t.font.text2, t.typography(14), t.margin.vertical(16)]
              );
            }.bind(void 0)
          )
        ),
        a = o.default.ul
          .attrs({ role: "listbox" })
          .withConfig({ componentId: "e1y7e-1" })([
          "padding-left:24px;margin:0;",
        ]);
      e.b = a;
    },
    1313: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return s;
      });
      var i = n(1),
        r = n(0),
        o = (n(6), n(43));
      function c(t) {
        var e = t.size,
          n = void 0 === e ? 24 : e;
        return Object(r.jsx)(o.b, {
          viewBoxSize: 48,
          size: n,
          currentColor: !0,
          path:
            "M24 4C12.96 4 4 12.96 4 24C4 35.04 12.96 44 24 44C35.04 44 44 35.04 44 24C44 12.96 35.04 4 24 4ZM26.3575 34.5078C26.3575 35.8885 25.2382 37.0078 23.8575 37.0078C22.4768 37.0078 21.3575 35.8885 21.3575 34.5078C21.3575 33.1271 22.4768 32.0078 23.8575 32.0078C25.2382 32.0078 26.3575 33.1271 26.3575 34.5078ZM26.4737 26.5479C26.4937 26.5079 26.5137 26.4679 26.5337 26.4479C27.078 25.6496 27.8363 24.9847 28.605 24.3108C30.3785 22.7559 32.2076 21.1522 31.5937 17.7479C31.0137 14.3679 28.3137 11.5879 24.9337 11.0879C20.8137 10.4879 17.1737 13.0279 16.0737 16.6679C15.7337 17.8279 16.6137 19.0079 17.8137 19.0079H18.2137C19.0337 19.0079 19.6937 18.4279 19.9737 17.7079C20.6137 15.9279 22.4937 14.7079 24.5737 15.1479C26.4937 15.5479 27.8937 17.4479 27.7137 19.4079C27.5782 20.9349 26.4873 21.7795 25.2767 22.7168C24.5213 23.3016 23.7192 23.9225 23.0737 24.7679L23.0537 24.7479C23.019 24.7825 22.9911 24.8305 22.962 24.8803C22.9408 24.9167 22.919 24.9541 22.8937 24.9879C22.8637 25.0379 22.8287 25.0879 22.7937 25.1379C22.7587 25.1879 22.7237 25.2379 22.6937 25.2879C22.5137 25.5679 22.3737 25.8479 22.2537 26.1679C22.2437 26.2079 22.2237 26.2379 22.2037 26.2679C22.1837 26.2979 22.1637 26.3279 22.1537 26.3679C22.1337 26.3879 22.1337 26.4079 22.1337 26.4279C21.8937 27.1479 21.7337 28.0079 21.7337 29.0279H25.7537C25.7537 28.5279 25.8137 28.0879 25.9537 27.6679C25.9937 27.5279 26.0537 27.3879 26.1137 27.2479C26.1337 27.1679 26.1537 27.0879 26.1937 27.0279C26.2737 26.8679 26.3737 26.7079 26.4737 26.5479Z",
          transform: "",
          fillRule: "evenodd",
          clipRule: "evenodd",
        });
      }
      var a = n(2);
      function s(t) {
        var e = t.size;
        return Object(r.jsx)(l, {
          size: e,
          children: Object(r.jsx)(u, {
            children: Object(r.jsx)(c, { size: e }),
          }),
        });
      }
      var l = a.default.div.withConfig({ componentId: "wluhbz-0" })(
          [
            "display:flex;align-items:center;justify-content:center;width:",
            "px;height:",
            "px;border-radius:1000px;color:",
            ";background-color:",
            ";",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.size;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.size;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text3;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.surface3;
          }.bind(void 0)
        ),
        u = a.default.div.withConfig({ componentId: "wluhbz-1" })([
          "transform:scale(0.8);",
        ]);
    },
    1314: function (t, e, n) {
      "use strict";
      n(28);
      var i = n(1),
        r = n(34),
        o = n(37),
        c = n(24),
        a = n(36),
        s = n(45),
        l = n(30),
        u = n(4),
        d = n(0),
        b = n(6),
        h = n.n(b),
        f = n(51),
        j = n(2),
        p = n(5),
        O = n(160),
        m = n(43);
      function v() {
        var t = Object(d.jsx)(d.Fragment, {
          children: Object(d.jsxs)("g", {
            fill: "#000",
            children: [
              Object(d.jsx)("path", {
                d:
                  "m14.9337 4.2052c.6898-.86282 1.1579-2.02134 1.0344-3.2052-1.0098.05021-2.2421.6662-2.9555 1.5297-.6406.73945-1.2075 1.94647-1.0597 3.0807 1.1335.09833 2.266-.56659 2.9808-1.4052z",
              }),
              Object(d.jsx)("path", {
                d:
                  "m15.9552 5.83187c-1.6462-.09806-3.0458.93429-3.832.93429-.7866 0-1.9904-.88488-3.29245-.86103-1.69471.02489-3.26721.98309-4.1272 2.50708-1.76886 3.04869-.4668 7.57109 1.25332 10.05409.83533 1.2284 1.84204 2.581 3.16855 2.5324 1.25328-.0492 1.74458-.8115 3.26798-.8115 1.5224 0 1.9649.8115 3.2916.7869 1.3759-.0246 2.236-1.229 3.0713-2.4586.9583-1.4004 1.3506-2.7525 1.3753-2.8267-.0247-.0246-2.653-1.0329-2.6774-4.0563-.0248-2.53154 2.0635-3.73568 2.1618-3.81035-1.1793-1.7442-3.0219-1.94087-3.6608-1.99028z",
              }),
            ],
          }),
        });
        return Object(d.jsx)(m.b, {
          viewBoxSize: 24,
          size: 24,
          currentColor: !0,
          path: t,
          transform: "",
        });
      }
      var g = n(721);
      function x() {
        var t = Object(d.jsxs)(d.Fragment, {
          children: [
            Object(d.jsx)("path", {
              d:
                "m20.822 12.2069c0-.6118-.0496-1.2269-.1554-1.8287h-8.4888v3.4656h4.8611c-.2017 1.1177-.8498 2.1065-1.7989 2.7348v2.2487h2.9001c1.7031-1.5675 2.6819-3.8823 2.6819-6.6204z",
              fill: "#4285f4",
            }),
            Object(d.jsx)("path", {
              d:
                "m12.1777 21.0001c2.4273 0 4.4742-.797 5.9657-2.1726l-2.9002-2.2487c-.8069.5489-1.8485.8598-3.0622.8598-2.34785 0-4.3386-1.584-5.05289-3.7137h-2.99273v2.3182c1.52778 3.039 4.63956 4.957 8.04232 4.957z",
              fill: "#34a853",
            }),
            Object(d.jsx)("path", {
              d:
                "m7.12487 13.7249c-.37698-1.1177-.37698-2.328 0-3.4458v-2.31809h-2.98943c-1.27645 2.54299-1.27645 5.53909 0 8.08199z",
              fill: "#fbbc04",
            }),
            Object(d.jsx)("path", {
              d:
                "m12.1777 6.56213c1.2831-.01985 2.5232.46296 3.4524 1.34921l2.5695-2.56946c-1.627-1.52778-3.7864-2.36773-6.0219-2.34127-3.40276 0-6.51454 1.91799-8.04232 4.96033l2.98942 2.31816c.71098-2.13297 2.70504-3.71697 5.0529-3.71697z",
              fill: "#ea4335",
            }),
          ],
        });
        return Object(d.jsx)(m.b, {
          viewBoxSize: 24,
          size: 24,
          currentColor: !0,
          path: t,
          transform: "",
        });
      }
      var y,
        w = n(720),
        k = n(14);
      function C(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(l.a)(t);
          if (e) {
            var r = Object(l.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(s.a)(this, n);
        };
      }
      !(function (t) {
        (t.Apple = "apple"),
          (t.Facebook = "facebook"),
          (t.GooglePlus = "googleplus"),
          (t.Twitter = "twitter"),
          (t.Weibo = "sina");
      })(y || (y = {}));
      var I = (function (t) {
        Object(a.a)(n, t);
        var e = C(n);
        function n() {
          var t,
            o = this;
          Object(r.a)(this, n);
          for (var a = arguments.length, s = new Array(a), l = 0; l < a; l++)
            s[l] = arguments[l];
          return (
            (t = e.call.apply(e, [this].concat(s))),
            Object(u.a)(
              Object(c.a)(t),
              "handleRedirectSignUp",
              function () {
                Object(i.a)(this, o),
                  (location.href = k.signup(
                    {},
                    { return_to: t.props.returnTo, ref: t.props.ref }
                  ));
              }.bind(this)
            ),
            Object(u.a)(
              Object(c.a)(t),
              "handleRedirectLogin",
              function () {
                Object(i.a)(this, o),
                  (location.href = k.login(
                    {},
                    { return_to: t.props.returnTo, ref: t.props.ref }
                  ));
              }.bind(this)
            ),
            t
          );
        }
        return (
          Object(o.a)(n, [
            {
              key: "render",
              value: function () {
                var t = this,
                  e = this.props,
                  n = e.token,
                  r = e.ref,
                  o = e.overlayHighContrast,
                  c = e.gigyaAuthUrl;
                return Object(d.jsx)(p.d, {
                  children: function (e) {
                    return (
                      Object(i.a)(this, t),
                      Object(d.jsxs)(d.Fragment, {
                        children: [
                          Object(d.jsxs)(S, {
                            children: [
                              Object(d.jsx)(O.a, {
                                onClick: this.handleRedirectSignUp,
                                primary: !o,
                                big: !0,
                                overlayHighContrast: o,
                                children: e("�㏂궖�╉꺍�덀굮鵝쒏닇"),
                              }),
                              Object(d.jsx)(O.a, {
                                onClick: this.handleRedirectLogin,
                                big: !0,
                                overlayHighContrast: o,
                                children: e("��궛�ㅳ꺍"),
                              }),
                            ],
                          }),
                          Object(d.jsxs)(P, {
                            children: [
                              Object(d.jsx)(R, {
                                overlayHighContrast: o,
                                children: e(
                                  "�곥겂�╉걚�뗣궋�ャ궑�녈깉�㎯겘�섅굙��"
                                ),
                              }),
                              Object(d.jsxs)(D, {
                                children: [
                                  Object(d.jsx)(_, {
                                    provider: y.Apple,
                                    returnTo: this.props.returnTo,
                                    token: n,
                                    ref: r,
                                    overlayHighContrast: o,
                                    gigyaAuthUrl: c,
                                  }),
                                  Object(d.jsx)(_, {
                                    provider: y.Twitter,
                                    returnTo: this.props.returnTo,
                                    token: n,
                                    ref: r,
                                    overlayHighContrast: o,
                                    gigyaAuthUrl: c,
                                  }),
                                  Object(d.jsx)(_, {
                                    provider: y.GooglePlus,
                                    returnTo: this.props.returnTo,
                                    token: n,
                                    ref: r,
                                    overlayHighContrast: o,
                                    gigyaAuthUrl: c,
                                  }),
                                  Object(d.jsx)(_, {
                                    provider: y.Facebook,
                                    returnTo: this.props.returnTo,
                                    token: n,
                                    ref: r,
                                    overlayHighContrast: o,
                                    gigyaAuthUrl: c,
                                  }),
                                ],
                              }),
                            ],
                          }),
                        ],
                      })
                    );
                  }.bind(this),
                });
              },
            },
          ]),
          n
        );
      })(h.a.Component);
      e.a = Object(f.connect)(function (t) {
        return { token: t.api.token, gigyaAuthUrl: k.accounts.gigyaAuth(t)() };
      })(I);
      function _(t) {
        var e = t.provider,
          n = t.returnTo,
          i = t.ref,
          r = t.overlayHighContrast,
          o = t.gigyaAuthUrl,
          c = t.token;
        return Object(d.jsxs)("form", {
          method: "POST",
          action: o,
          children: [
            Object(d.jsx)("input", {
              type: "hidden",
              name: "mode",
              value: "signin",
            }),
            Object(d.jsx)("input", {
              type: "hidden",
              name: "provider",
              value: e,
            }),
            Object(d.jsx)("input", {
              type: "hidden",
              name: "source",
              value: "pc",
            }),
            Object(d.jsx)("input", {
              type: "hidden",
              name: "view_type",
              value: "page",
            }),
            void 0 !== n &&
              Object(d.jsx)("input", {
                type: "hidden",
                name: "return_to",
                value: n,
              }),
            void 0 !== i &&
              Object(d.jsx)("input", { type: "hidden", name: "ref", value: i }),
            void 0 !== c &&
              Object(d.jsx)("input", { type: "hidden", name: "tt", value: c }),
            Object(d.jsx)(T, {
              type: "submit",
              overlayHighContrast: r,
              children:
                e === y.Apple
                  ? Object(d.jsx)(v, {})
                  : e === y.Twitter
                  ? Object(d.jsx)(w.a, {})
                  : e === y.GooglePlus
                  ? Object(d.jsx)(x, {})
                  : Object(d.jsx)(g.a, {}),
            }),
          ],
        });
      }
      var S = j.default.div.withConfig({ componentId: "sc-15ip9ll-0" })([
          "display:flex;flex-flow:column;align-items:stretch;justify-content:space-between;height:88px;margin-bottom:20px;",
        ]),
        P = j.default.div.withConfig({ componentId: "sc-15ip9ll-1" })([
          "display:flex;flex-flow:column;align-items:center;justify-content:space-between;height:74px;",
        ]),
        D = j.default.div.withConfig({ componentId: "sc-15ip9ll-2" })([
          "display:flex;align-items:center;justify-content:space-between;margin:0 auto;width:184px;",
        ]),
        T = j.default.button.withConfig({ componentId: "sc-15ip9ll-3" })(
          [
            "",
            " display:flex;align-items:center;justify-content:center;cursor:pointer;width:40px;height:40px;border-radius:50%;border:none;outline:none;transition:background-color 0.2s;",
          ],
          function (t) {
            var e = this;
            return (
              Object(i.a)(this, void 0),
              t.overlayHighContrast
                ? Object(j.css)([
                    "background:rgba(255,255,255,0.2);color:white;&:not(:disabled):not(.forceBubbling){&:hover,&:focus{background-color:rgba(255,255,255,0.4);}}",
                  ])
                : Object(j.css)(
                    ["", ""],
                    function (t) {
                      return Object(i.a)(this, e), t.theme.ui.element.default;
                    }.bind(this)
                  )
            );
          }.bind(void 0)
        ),
        R = j.default.div.withConfig({ componentId: "sc-15ip9ll-4" })(
          [
            "",
            " font-size:14px;line-height:22px;padding:0 16px;margin:0 auto 12px;",
          ],
          function (t) {
            var e = this;
            return (
              Object(i.a)(this, void 0),
              t.overlayHighContrast
                ? Object(j.css)(["color:white;"])
                : Object(j.css)(
                    ["color:", ";"],
                    function (t) {
                      return Object(i.a)(this, e), t.theme.text.subLink;
                    }.bind(this)
                  )
            );
          }.bind(void 0)
        );
    },
    1319: function (t, e, n) {
      "use strict";
      n.d(e, "b", function () {
        return $;
      }),
        n.d(e, "a", function () {
          return et;
        });
      var i = n(1),
        r = n(8),
        o = n(0),
        c = n(9),
        a = n(6),
        s = n.n(a),
        l = n(5),
        u = n(2),
        d = n(15),
        b = n(26),
        h = n(524),
        f = n(95),
        j = n(219),
        p =
          (n(111),
          n(47),
          n(31),
          n(54),
          n(65),
          n(189),
          n(29),
          n(81),
          n(58),
          n(200),
          n(201),
          n(202),
          n(203),
          n(204),
          n(205),
          n(206),
          n(207),
          n(208),
          n(209),
          n(210),
          n(211),
          n(212),
          n(60),
          n(11)),
        O = n(308);
      function m(t, e) {
        var n;
        if ("undefined" == typeof Symbol || null == t[Symbol.iterator]) {
          if (
            Array.isArray(t) ||
            (n = (function (t, e) {
              if (!t) return;
              if ("string" == typeof t) return v(t, e);
              var n = Object.prototype.toString.call(t).slice(8, -1);
              "Object" === n && t.constructor && (n = t.constructor.name);
              if ("Map" === n || "Set" === n) return Array.from(t);
              if (
                "Arguments" === n ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)
              )
                return v(t, e);
            })(t)) ||
            (e && t && "number" == typeof t.length)
          ) {
            n && (t = n);
            var i = 0,
              r = function () {};
            return {
              s: r,
              n: function () {
                return i >= t.length
                  ? { done: !0 }
                  : { done: !1, value: t[i++] };
              },
              e: function (t) {
                throw t;
              },
              f: r,
            };
          }
          throw new TypeError(
            "Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
          );
        }
        var o,
          c = !0,
          a = !1;
        return {
          s: function () {
            n = t[Symbol.iterator]();
          },
          n: function () {
            var t = n.next();
            return (c = t.done), t;
          },
          e: function (t) {
            (a = !0), (o = t);
          },
          f: function () {
            try {
              c || null == n.return || n.return();
            } finally {
              if (a) throw o;
            }
          },
        };
      }
      function v(t, e) {
        (null == e || e > t.length) && (e = t.length);
        for (var n = 0, i = new Array(e); n < e; n++) i[n] = t[n];
        return i;
      }
      function g(t) {
        var e,
          n = this,
          r = t.poll,
          c = r.total,
          a = new Map(),
          s = 1,
          l = m(r.choices);
        try {
          for (l.s(); !(e = l.n()).done; ) {
            var u = e.value,
              d = c > 0 ? Math.round((u.count / c) * 100) : 0,
              b = "".concat(d).length;
            s < b && (s = b), a.set(u.id, d);
          }
        } catch (h) {
          l.e(h);
        } finally {
          l.f();
        }
        return Object(o.jsx)(x, {
          children: r.choices.map(
            function (t) {
              Object(i.a)(this, n);
              var e = a.get(t.id);
              return void 0 !== e
                ? Object(o.jsx)(
                    y,
                    {
                      id: t.id,
                      count: t.count,
                      text: t.text,
                      selectedValue: r.selectedValue,
                      rate: e,
                      maxDigits: s,
                    },
                    t.id
                  )
                : null;
            }.bind(this)
          ),
        });
      }
      var x = u.default.ul.withConfig({ componentId: "j1vbd9-0" })([
          "list-style:none;margin:16px 0 0;padding:0;",
        ]),
        y = s.a.memo(function (t) {
          var e = t.id,
            n = t.count,
            i = t.text,
            r = t.selectedValue,
            c = t.rate,
            a = t.maxDigits,
            s = Object(p.y)()(n);
          return Object(o.jsxs)(N, {
            children: [
              Object(o.jsx)(D, { children: Object(o.jsx)(T, { rate: c }) }),
              Object(o.jsxs)(w, {
                children: [
                  Object(o.jsx)(k, {
                    children: Object(o.jsxs)(C, {
                      children: [
                        Object(o.jsx)(z, {
                          selected: e === r,
                          children: Object(o.jsx)(O.a, {}),
                        }),
                        Object(o.jsx)(R, { children: i }),
                      ],
                    }),
                  }),
                  Object(o.jsxs)(I, {
                    children: [
                      Object(o.jsx)(_, { children: s }),
                      Object(o.jsx)(S, { children: "/" }),
                      Object(o.jsxs)(P, {
                        style: { minWidth: 8 * a + 13 },
                        children: [c, "%"],
                      }),
                    ],
                  }),
                ],
              }),
            ],
          });
        }),
        w = u.default.div.withConfig({ componentId: "j1vbd9-1" })([
          "z-index:1;display:flex;margin:0 6px;width:100%;",
        ]),
        k = u.default.div.withConfig({ componentId: "j1vbd9-2" })([
          "width:100%;",
        ]),
        C = u.default.div.withConfig({ componentId: "j1vbd9-3" })([
          "display:flex;align-items:center;flex-grow:1;",
        ]),
        I = u.default.ul.withConfig({ componentId: "j1vbd9-4" })(
          ["display:flex;color:", ";list-style:none;align-items:center;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        _ = u.default.li.withConfig({ componentId: "j1vbd9-5" })([
          "margin-right:4px;",
        ]),
        S = u.default.li.withConfig({ componentId: "j1vbd9-6" })([
          "margin-right:4px;",
        ]),
        P = u.default.li.withConfig({ componentId: "j1vbd9-7" })([
          "display:inline-block;text-align:right;",
        ]),
        D = u.default.div.withConfig({ componentId: "j1vbd9-8" })([
          "position:absolute;width:100%;height:100%;",
        ]),
        T = u.default.div.withConfig({ componentId: "j1vbd9-9" })(
          [
            "min-width:8px;width:",
            "%;height:100%;background-color:",
            ";border-radius:4px;",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.rate;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.surface3;
          }.bind(void 0)
        ),
        R = u.default.span.withConfig({ componentId: "j1vbd9-10" })(
          ["font-weight:bold;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        z = u.default.div.withConfig({ componentId: "j1vbd9-11" })(
          ["margin-right:4px;display:flex;opacity:0;color:", ";", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text3;
          }.bind(void 0),
          function (t) {
            return (
              Object(i.a)(this, void 0),
              t.selected && Object(u.css)(["opacity:1;"])
            );
          }.bind(void 0)
        ),
        N = u.default.li.withConfig({ componentId: "j1vbd9-12" })([
          "display:flex;position:relative;z-index:0;justify-content:space-between;align-items:center;min-height:28px;margin-top:4px;height:40px;",
        ]),
        M = (n(28), n(34)),
        q = n(37),
        U = n(24),
        E = n(36),
        L = n(45),
        A = n(30),
        F = n(4),
        B = n(87),
        W = n(77),
        H = n(1327);
      function V(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(A.a)(t);
          if (e) {
            var r = Object(A.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(L.a)(this, n);
        };
      }
      function G(t) {
        var e = this,
          n = t.show,
          a = t.data,
          s = t.onClose,
          u = t.onSubmit,
          d = Object(l.p)(l.m.work.ns),
          b = Object(r.a)(d, 1)[0],
          h = Object(c.a)(
            function (t) {
              Object(i.a)(this, e), u(t), s();
            }.bind(this),
            [u, s]
          );
        return Object(o.jsxs)(B.e, {
          show: n,
          onBackgroundClick: s,
          children: [
            Object(o.jsx)(B.d, {
              onCloseClick: s,
              children: b(l.m.work.info_by_user.questionnaire),
            }),
            Object(o.jsx)(B.a, {
              children: Object(o.jsxs)("div", {
                className: H.listContainer,
                children: [
                  Object(o.jsx)("h2", {
                    className: H.question,
                    children: a.question,
                  }),
                  Object(o.jsx)("ul", {
                    className: H.list,
                    children: a.choices.map(
                      function (t) {
                        return (
                          Object(i.a)(this, e),
                          Object(o.jsx)(
                            Z,
                            { id: t.id, text: t.text, onClick: h },
                            t.id
                          )
                        );
                      }.bind(this)
                    ),
                  }),
                ],
              }),
            }),
          ],
        });
      }
      var Z = (function (t) {
          Object(E.a)(n, t);
          var e = V(n);
          function n() {
            var t,
              r = this;
            Object(M.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(F.a)(
                Object(U.a)(t),
                "handleClick",
                function () {
                  Object(i.a)(this, r);
                  var e = t.props,
                    n = e.id;
                  (0, e.onClick)(n);
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(q.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this.props.text;
                  return Object(o.jsxs)(Y, {
                    onClick: this.handleClick,
                    children: [
                      Object(o.jsx)(O.a, {}),
                      Object(o.jsx)(X, { children: t }),
                    ],
                  });
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        Y = u.default.li.withConfig({ componentId: "sc-191hwn4-0" })(
          [
            "height:40px;padding:0 8px;display:flex;align-items:center;font-size:14px;background-color:",
            ";color:",
            ";transition:0.2s background-color;cursor:pointer;svg{transition:0.2s opacity;opacity:0;}&:hover{background-color:",
            ";svg{opacity:1;}}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.background1;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0),
          function (t) {
            return (
              Object(i.a)(this, void 0),
              Object(W.a)(t.theme.materials.background1, t.theme.effects.hover)
            );
          }.bind(void 0)
        ),
        X = u.default.div.withConfig({ componentId: "sc-191hwn4-1" })([
          "font-size:14px;line-height:22px;margin-left:4px;",
        ]);
      function $(t) {
        var e = this,
          n = t.isOwnWork,
          a = t.pollData,
          s = t.onSubmit,
          u = Object(l.p)([l.m.work.ns]),
          d = Object(r.a)(u, 1)[0],
          b = Object(c.j)(!1),
          h = Object(r.a)(b, 2),
          f = h[0],
          p = h[1],
          O = Object(c.a)(
            function () {
              Object(i.a)(this, e), a.selectedValue || n || p(!0);
            }.bind(this),
            [n, a.selectedValue]
          ),
          m = Object(c.a)(
            function () {
              Object(i.a)(this, e), p(!1);
            }.bind(this),
            []
          );
        return Object(o.jsxs)(o.Fragment, {
          children: [
            Object(o.jsxs)(K, {
              selected: null !== a.selectedValue || n,
              onClick: O,
              children: [
                Object(o.jsxs)(Q, {
                  children: [
                    Object(o.jsxs)("div", {
                      children: [
                        Object(o.jsx)(J, {
                          children: d(l.m.work.info_by_user.questionnaire),
                        }),
                        Object(o.jsx)(tt, { children: a.question }),
                      ],
                    }),
                    Object(o.jsx)("div", {
                      children: Object(o.jsx)(j.c, { value: a.total }),
                    }),
                  ],
                }),
                (a.selectedValue || n) && Object(o.jsx)(g, { poll: a }),
              ],
            }),
            Object(o.jsx)(G, { data: a, show: f, onSubmit: s, onClose: m }),
          ],
        });
      }
      var K = u.default.div.withConfig({ componentId: "depkkw-0" })(
          [
            "box-sizing:border-box;margin:16px 0 24px;padding:16px;width:100%;border-radius:4px;border:1px solid ",
            ";background-color:",
            ";cursor:pointer;",
            "",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.ui.border.default;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.background.clear;
          }.bind(void 0),
          function (t) {
            return (
              Object(i.a)(this, void 0),
              t.selected && Object(u.css)(["cursor:initial;"])
            );
          }.bind(void 0)
        ),
        Q = u.default.div.withConfig({ componentId: "depkkw-1" })([
          "display:flex;justify-content:space-between;align-items:center;",
        ]),
        J = u.default.div.withConfig({ componentId: "depkkw-2" })(
          ["margin:0 0 4px;font-size:12px;font-weight:bold;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text3;
          }.bind(void 0)
        ),
        tt = u.default.div.withConfig({ componentId: "depkkw-3" })(
          ["font-weight:bold;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text1;
          }.bind(void 0)
        );
      function et(t) {
        var e = this,
          n = t.workType,
          r = t.id,
          a = Object(d.b)(),
          s = Object(d.c)(
            function (t) {
              return Object(i.a)(this, e), Object(f.c)(t, n, r);
            }.bind(this)
          ),
          l = Object(d.c)(b.k),
          u = Object(d.c)(
            function (t) {
              return Object(i.a)(this, e), Object(h.a)(t, n, r);
            }.bind(this)
          ),
          j = Object(c.a)(
            function (t) {
              Object(i.a)(this, e);
              try {
                a(Object(h.d)(n, r, t));
              } catch (o) {
                a(Object(h.e)(n, r, t));
              }
            }.bind(this),
            [a, r, n]
          );
        return u
          ? Object(o.jsx)($, {
              isOwnWork: l === (s && s.userId),
              pollData: u,
              onSubmit: j,
            })
          : null;
      }
    },
    1322: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return b;
      });
      n(117), n(150), n(54), n(53), n(103);
      var i = n(1),
        r = n(8),
        o = n(0),
        c = n(9),
        a = (n(6), n(42)),
        s = n(870),
        l = n(2),
        u = n(5),
        d = n(11);
      function b(t) {
        var e = this,
          n = t.fanboxPromotion,
          s = t.isLoggedIn,
          l = Object(u.o)(),
          b = Object(r.a)(l, 1)[0],
          w = Object(a.o)(),
          k = n.userName,
          C = n.contentUrl,
          I = n.imageUrl,
          _ = n.description,
          S = Object(c.g)(
            function () {
              return Object(i.a)(this, e), I && w && s;
            }.bind(this),
            [I, s, w]
          ),
          P = b("%(userName)�췋ANBOX", { userName: k }),
          D = P.length > 13 ? "".concat(P.slice(0, 12), "��") : P,
          T =
            _.length > 40
              ? "".concat(_.replace(/\r?\n/, "").slice(0, 39), "��")
              : _;
        return Object(o.jsxs)(g, {
          children: [
            S
              ? Object(o.jsxs)(o.Fragment, {
                  children: [
                    Object(o.jsx)(h, { children: b("pixivFANBOX") }),
                    Object(o.jsxs)(x, {
                      href: C,
                      target: "_blank",
                      rel: "noopener",
                      className: "gtm-work-aside-fanbox",
                      children: [
                        Object(o.jsx)(O, {
                          style: { backgroundImage: Object(d.d)(I) },
                        }),
                        Object(o.jsxs)(m, {
                          children: [
                            Object(o.jsx)(p, { children: D }),
                            Object(o.jsx)(j, { children: b("��뤃�쇻굥") }),
                          ],
                        }),
                      ],
                    }),
                  ],
                })
              : Object(o.jsx)(y, {
                  children: Object(o.jsxs)(m, {
                    children: [
                      Object(o.jsxs)(v, {
                        children: [
                          Object(o.jsx)(h, { children: b("pixivFANBOX") }),
                          Object(o.jsx)("a", {
                            href: C,
                            target: "_blank",
                            rel: "noopener referer",
                            className: "gtm-work-aside-fanbox",
                            children: Object(o.jsx)(p, { children: D }),
                          }),
                        ],
                      }),
                      Object(o.jsx)("a", {
                        href: C,
                        target: "_blank",
                        rel: "noopener referer",
                        className: "gtm-work-aside-fanbox",
                        children: Object(o.jsx)(j, {
                          children: b("��뤃�쇻굥"),
                        }),
                      }),
                    ],
                  }),
                }),
            Object(o.jsx)(f, { children: T }),
          ],
        });
      }
      var h = l.default.p.withConfig({ componentId: "sc-1u16qqh-0" })(
          ["color:", ";font-size:14px;line-height:22px;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text3;
          }.bind(void 0)
        ),
        f = l.default.div.withConfig({ componentId: "sc-1u16qqh-1" })(
          ["margin-top:4px;line-height:22px;font-size:14px;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        j = l.default.div.withConfig({ componentId: "sc-1u16qqh-2" })(
          [
            "display:flex;flex:none;align-items:center;justify-content:center;width:88px;height:32px;background:linear-gradient(90deg,#d1ff1a 0%,#1ad1ff 100%);border-radius:20px;color:",
            ";font-size:14px;line-height:22px;font-weight:bold;",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text5;
          }.bind(void 0)
        ),
        p = l.default.div.withConfig({ componentId: "sc-1u16qqh-3" })([
          "display:flex;flex-flow:column;align-items:flex-start;word-break:break-all;line-height:22px;font-size:14px;font-weight:bold;text-decoration:none;",
        ]),
        O = l.default.div.withConfig({ componentId: "sc-1u16qqh-4" })(
          [
            "position:relative;transition:0.2s opacity;margin-bottom:7px;border-radius:4px;background-repeat:no-repeat;background-position:center center;background-size:cover;background-color:",
            ";width:100%;height:178px;&:hover{opacity:0.8;}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.background.overlay;
          }.bind(void 0)
        ),
        m = l.default.div.withConfig({ componentId: "sc-1u16qqh-5" })([
          "display:flex;justify-content:space-between;flex-flow:row;width:100%;",
        ]),
        v = l.default.div.withConfig({ componentId: "sc-1u16qqh-6" })([
          "display:flex;flex-flow:column;margin-right:8px;",
        ]),
        g = Object(l.default)(s.a).withConfig({ componentId: "sc-1u16qqh-7" })([
          "padding:16px 0 36px;",
        ]),
        x = l.default.a.withConfig({ componentId: "sc-1u16qqh-8" })(
          [
            "display:block;position:relative;width:100%;height:178px;text-decoration:none;",
            "{position:absolute;align-items:center;background:linear-gradient(to top,rgba(0,0,0,0.32) 0%,rgba(0,0,0,0) 100%);border-radius:0 0 4px 4px;bottom:0;",
            "{margin:8px 8px 8px 0;}",
            "{margin:13px 0 13px 8px;color:",
            ";}}",
          ],
          m,
          j,
          p,
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text5;
          }.bind(void 0)
        ),
        y = l.default.div.withConfig({ componentId: "sc-1u16qqh-9" })(
          [
            "display:block;position:relative;width:100%;",
            "{align-items:flex-end;",
            "{color:",
            ";}",
            "{margin:6px 0;}}",
          ],
          m,
          p,
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0),
          j
        );
    },
    1323: function (t, e, n) {
      "use strict";
      function i(t) {
        return t.get("/ajax/linked_service", {}, {});
      }
      function r(t, e) {
        return t.get("/ajax/linked_service/tumeng", {}, { page: e });
      }
      function o(t, e) {
        return t.get(
          "/ajax/linked_service/tumeng/illust_state",
          {},
          { illustId: e }
        );
      }
      function c(t, e, n) {
        return t.post(
          "/ajax/linked_service/tumeng/state",
          {},
          { id: e, action: n }
        );
      }
      function a(t, e) {
        return t.post(
          "/ajax/linked_service/tumeng/user_state",
          {},
          { action: e }
        );
      }
      function s(t) {
        return t.get("/ajax/linked_service/tumeng/info", {}, {});
      }
      function l(t) {
        return t.get("/ajax/user/twitter_account", {}, {});
      }
      function u(t) {
        return t.post("/ajax/user/twitter_account/disconnect", {}, {});
      }
      n.d(e, "e", function () {
        return i;
      }),
        n.d(e, "b", function () {
          return r;
        }),
        n.d(e, "c", function () {
          return o;
        }),
        n.d(e, "g", function () {
          return c;
        }),
        n.d(e, "h", function () {
          return a;
        }),
        n.d(e, "d", function () {
          return s;
        }),
        n.d(e, "f", function () {
          return l;
        }),
        n.d(e, "a", function () {
          return u;
        });
    },
    1324: function (t, e, n) {
      t.exports = {
        wrapper: "_36ao1oi",
        reverse: "_2Dw8FaW",
        nav: "_3xvTuCS",
        tab: "_3RIIBp0",
        current: "_2aTzkEz",
      };
    },
    1325: function (t, e, n) {
      "use strict";
      n(29), n(70), n(176);
      var i,
        r = n(1),
        o = n(7),
        c = n.n(o),
        a = (n(39), n(12)),
        s = n(8),
        l = n(4),
        u = n(0),
        d = n(9),
        b = n(6),
        h = n.n(b),
        f = n(5),
        j = n(11),
        p = n(51),
        O = n(3),
        m = n(95),
        v = n(593),
        g = n(160),
        x = n(546),
        y = n(1314),
        w = n(101),
        k = n(379),
        C = n(2),
        I = n(175),
        _ = n(424),
        S = n(467);
      function P(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function D(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? P(Object(n), !0).forEach(function (e) {
                Object(l.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : P(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function T(t) {
        var e = Object(_.b)(_.a.ForGeneral);
        if (void 0 === t.work) return null;
        var n = t.reason,
          i = t.onMuteClick,
          r = t.onRevealClick;
        if (t.work.type === O.q.Illust) {
          var o = t.work,
            c = o.title,
            a = o.urls.mini,
            s = o.width,
            l = o.height,
            d = o.xRestrict,
            b = n === S.a.UserAge || !1 !== e;
          return Object(u.jsxs)(J, {
            bookmarkable: !0,
            mask: b,
            blur: !b,
            children: [
              Object(u.jsx)(V, {
                children: Object(u.jsxs)(G, {
                  children: [
                    Object(u.jsx)(x.a, { size: 72, currentColor: !0 }),
                    n === S.a.UserAge
                      ? Object(u.jsx)(R, {})
                      : n === S.a.XRestrict
                      ? Object(u.jsx)(z, { xRestrict: d })
                      : n === S.a.Mute
                      ? Object(u.jsx)(q, { onClick: i })
                      : n === S.a.Spoiler
                      ? Object(u.jsx)(U, { onClick: r })
                      : n === S.a.XRestrictForNoLogin
                      ? Object(u.jsx)(N, { xRestrict: d, mask: b, blur: !b })
                      : n === S.a.NotSafeForNoLogin
                      ? Object(u.jsx)(M, { mask: b, blur: !b })
                      : Object(j.v)(n),
                  ],
                }),
              }),
              !b && Object(u.jsx)(E, { alt: c, src: a, width: s, height: l }),
            ],
          });
        }
        var h = t.work.xRestrict;
        return Object(u.jsx)(J, {
          mask: !0,
          bookmarkable: !0,
          children: Object(u.jsxs)(V, {
            children: [
              Object(u.jsx)(x.a, { size: 72, currentColor: !0 }),
              n === S.a.UserAge
                ? Object(u.jsx)(L, {})
                : n === S.a.XRestrict
                ? Object(u.jsx)(A, { xRestrict: h })
                : n === S.a.Mute
                ? Object(u.jsx)(B, { onClick: i })
                : n === S.a.Spoiler
                ? Object(u.jsx)(W, { onClick: r })
                : n === S.a.NotSafeForNoLogin || n === S.a.XRestrictForNoLogin
                ? Object(u.jsx)(F, { xRestrict: h })
                : Object(j.v)(n),
            ],
          }),
        });
      }
      function R() {
        var t = Object(f.p)(f.m.work.ns),
          e = Object(s.a)(t, 1)[0];
        return Object(u.jsx)(Z, {
          mask: !0,
          children: e(f.m.work.info_by_user.cannot_be_shown_to_under_18),
        });
      }
      function z(t) {
        var e = Object(f.p)([f.m.work.ns, f.m.action.ns]),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              blur: !0,
              children:
                t.xRestrict === O.r.R18G
                  ? n(f.m.work.info_by_user.you_disabled_r18g_work)
                  : n(f.m.work.info_by_user.you_disabled_r18_work),
            }),
            Object(u.jsx)(X, {
              children: Object(u.jsx)($, {
                href: Object(v.user)(),
                children: n(f.m.action.link.edit_user_setting),
              }),
            }),
          ],
        });
      }
      function N(t) {
        var e = Object(f.p)(f.m.work.ns),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              blur: t.blur,
              mask: t.mask,
              children:
                t.xRestrict === O.r.R18G
                  ? n(f.m.work.info_by_user.r18g_work_cannot_be_shown)
                  : n(f.m.work.info_by_user.r18_work_cannot_be_shown),
            }),
            Object(u.jsx)(Y, {
              mask: t.mask,
              blur: t.blur,
              children: n(
                f.m.work.info_by_user.require_account_to_show_r18_work
              ),
            }),
            Object(u.jsx)(K, {
              children: Object(u.jsx)(y.a, {
                returnTo: location.href,
                overlayHighContrast: t.blur,
              }),
            }),
          ],
        });
      }
      function M(t) {
        var e = Object(f.p)(f.m.work.ns),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              blur: t.blur,
              mask: t.mask,
              children: n(
                f.m.work.info_by_pixiv.sensitive_work_cannot_be_shown
              ),
            }),
            Object(u.jsx)(K, {
              children: Object(u.jsx)(y.a, {
                returnTo: location.href,
                overlayHighContrast: t.blur,
              }),
            }),
          ],
        });
      }
      function q(t) {
        var e = Object(f.p)([f.m.action.ns, f.m.work.ns]),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              blur: !0,
              children: n(f.m.work.info_by_user.this_work_is_muted),
            }),
            Object(u.jsx)(X, {
              children: Object(u.jsx)(g.a, {
                onClick: t.onClick,
                overlayHighContrast: !0,
                big: !0,
                children: n(f.m.action.link.open_mute_setting),
              }),
            }),
          ],
        });
      }
      function U(t) {
        var e = Object(f.p)([f.m.work.ns, f.m.action.ns]),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              blur: !0,
              children: n(
                f.m.work.info_by_pixiv.this_work_contains_spoiler_keyword
              ),
            }),
            Object(u.jsx)(X, {
              children: Object(u.jsx)(g.a, {
                onClick: t.onClick,
                overlayHighContrast: !0,
                big: !0,
                children: n(f.m.action.link.uncover_work),
              }),
            }),
          ],
        });
      }
      function E(t) {
        return Object(u.jsxs)(h.a.Suspense, {
          fallback: null,
          children: [Object(u.jsx)(H, D({}, t)), Object(u.jsx)(tt, {})],
        });
      }
      function L() {
        var t = Object(f.p)(f.m.work.ns),
          e = Object(s.a)(t, 1)[0];
        return Object(u.jsx)(Z, {
          children: e(f.m.work.info_by_user.cannot_be_shown_to_under_18),
        });
      }
      function A(t) {
        var e = Object(f.p)([f.m.work.ns, f.m.action.ns]),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              children:
                t.xRestrict === O.r.R18G
                  ? n(f.m.work.info_by_user.you_disabled_r18g_work)
                  : n(f.m.work.info_by_user.you_disabled_r18_work),
            }),
            Object(u.jsx)(X, {
              children: Object(u.jsx)($, {
                href: Object(v.user)(),
                children: n(f.m.action.link.open_restriction_setting),
              }),
            }),
          ],
        });
      }
      function F(t) {
        var e = Object(f.p)(f.m.work.ns),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              children:
                t.xRestrict === O.r.R18G
                  ? n(f.m.work.info_by_user.r18g_work_cannot_be_shown)
                  : n(f.m.work.info_by_user.r18_work_cannot_be_shown),
            }),
            Object(u.jsx)(Y, {
              children: n(
                f.m.work.info_by_user.require_account_to_show_r18_work
              ),
            }),
            Object(u.jsx)(K, {
              children: Object(u.jsx)(y.a, { returnTo: location.href }),
            }),
          ],
        });
      }
      function B(t) {
        var e = Object(f.p)([f.m.work.ns, f.m.action.ns]),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              children: n(f.m.work.info_by_user.this_work_is_muted),
            }),
            Object(u.jsx)(X, {
              children: Object(u.jsx)(Q, {
                type: "button",
                onClick: t.onClick,
                children: n(f.m.action.link.open_mute_setting),
              }),
            }),
          ],
        });
      }
      function W(t) {
        var e = Object(f.p)([f.m.work.ns, f.m.action.ns]),
          n = Object(s.a)(e, 1)[0];
        return Object(u.jsxs)(u.Fragment, {
          children: [
            Object(u.jsx)(Z, {
              children: n(
                f.m.work.info_by_pixiv.this_work_contains_spoiler_keyword
              ),
            }),
            Object(u.jsx)(X, {
              children: Object(u.jsx)(Q, {
                type: "button",
                onClick: t.onClick,
                children: n(f.m.action.link.uncover_work),
              }),
            }),
          ],
        });
      }
      e.a = Object(p.connect)(
        function (t, e) {
          var n = Object(m.c)(t, e.workType, e.id);
          return { work: n, reason: void 0 !== n ? Object(S.c)(t, n) : void 0 };
        },
        function (t, e) {
          return {
            onMuteClick: function () {
              return t(
                w.k.show(
                  e.workType === O.q.Illust
                    ? w.k.MuteMode.Illust
                    : e.workType === O.q.Novel
                    ? w.k.MuteMode.Novel
                    : Object(j.v)(e.workType),
                  e.id
                )
              );
            },
            onRevealClick: function () {
              return t(Object(k.c)(e.workType, e.id));
            },
          };
        }
      )(function (t) {
        if (void 0 === t.work) return t.fallback || null;
        var e = t.reason;
        return "function" == typeof t.children
          ? (0, t.children)(
              e,
              void 0 === e
                ? void 0
                : Object(u.jsx)(T, D(D({}, t), {}, { reason: e }))
            )
          : void 0 === e
          ? t.children
          : Object(u.jsx)(T, D(D({}, t), {}, { reason: e }));
      });
      var H = h.a.memo(function (t) {
          var e = this,
            o =
              (void 0 === i &&
                (i = {
                  promise: Object(a.a)(
                    c.a.mark(function t() {
                      var e;
                      return c.a.wrap(
                        function (t) {
                          for (;;)
                            switch ((t.prev = t.next)) {
                              case 0:
                                return (
                                  (t.prev = 0),
                                  (t.next = 3),
                                  Promise.resolve().then(function () {
                                    if (!n.m[1366]) {
                                      var t = new Error(
                                        "Module '1366' is not available (weak dependency)"
                                      );
                                      throw ((t.code = "MODULE_NOT_FOUND"), t);
                                    }
                                    return n(1366);
                                  })
                                );
                              case 3:
                                (e = t.sent),
                                  (i.current = e.illustCss),
                                  (t.next = 10);
                                break;
                              case 7:
                                (t.prev = 7),
                                  (t.t0 = t.catch(0)),
                                  (i.current = null);
                              case 10:
                              case "end":
                                return t.stop();
                            }
                        },
                        t,
                        null,
                        [[0, 7]]
                      );
                    })
                  )(),
                  current: void 0,
                }),
              i),
            s = o.current,
            l = o.promise,
            b = Object(d.g)(
              function () {
                return (
                  Object(r.a)(this, e),
                  s &&
                    C.default.img.withConfig({ componentId: "sc-3xfm45-0" })(
                      [
                        "",
                        " position:absolute;top:0;left:0;right:0;bottom:0;width:100%;min-height:calc(100% + 128px);object-fit:cover;filter:blur(32px);",
                      ],
                      s
                    )
                );
              }.bind(this),
              [s]
            );
          if (void 0 === b) throw l;
          return null === b ? null : Object(u.jsx)(b, D({}, t));
        }),
        V = C.default.div.withConfig({ componentId: "sc-3xfm45-1" })([
          "display:flex;flex-flow:column;align-items:center;padding:104px 48px;cursor:initial;",
        ]),
        G = C.default.div.withConfig({ componentId: "sc-3xfm45-2" })([
          "display:flex;flex-flow:column;align-items:center;justify-content:center;z-index:1;",
        ]),
        Z = Object(C.default)(
          Object(I.e)("span", ["mask", "blur"])
        ).withConfig({ componentId: "sc-3xfm45-3" })(
          ["color:", ";margin-top:16px;font-weight:bold;"],
          function (t) {
            Object(r.a)(this, void 0);
            var e = t.mask,
              n = t.blur,
              i = t.theme;
            return e ? i.text.subLink : n ? i.colors.text5 : i.text.whiteGray;
          }.bind(void 0)
        ),
        Y = Object(C.default)(
          Object(I.e)("span", ["mask", "blur"])
        ).withConfig({ componentId: "sc-3xfm45-4" })(
          ["color:", ";margin-top:8px;font-size:14px;line-height:22px;"],
          function (t) {
            Object(r.a)(this, void 0);
            var e = t.mask,
              n = t.blur,
              i = t.theme;
            return e
              ? i.text.subLink
              : n
              ? i.text.highContrast
              : i.text.whiteGray;
          }.bind(void 0)
        ),
        X = C.default.div.withConfig({ componentId: "sc-3xfm45-5" })([
          "display:flex;flex-flow:column;align-content:stretch;margin-top:36px;width:288px;",
        ]),
        $ = C.default.a.withConfig({ componentId: "sc-3xfm45-6" })([
          "display:flex;align-items:center;justify-content:center;border:none;color:#fff;text-decoration:none;transition:0.2s background-color;line-height:1;font-size:14px;font-weight:bold;cursor:pointer;",
        ]),
        K = C.default.div.withConfig({ componentId: "sc-3xfm45-7" })([
          "padding-top:36px;width:288px;",
        ]),
        Q = $.withComponent("button"),
        J = Object(C.default)(
          Object(I.e)("div", ["mask", "blur", "bookmarkable"])
        ).withConfig({ componentId: "sc-3xfm45-8" })(
          [
            "position:relative;overflow:hidden;display:flex;flex-flow:column;align-items:center;justify-content:center;color:",
            ";border-radius:8px 8px 0 0;",
            " ",
            " ",
            " ",
            "{font-size:",
            ";line-height:",
            ";}",
            ",",
            "{background-color:",
            ";position:relative;bottom:auto;width:288px;height:40px;border-radius:20px;font-size:auto;&:hover,&:focus{background-color:",
            ";}}",
          ],
          function (t) {
            return Object(r.a)(this, void 0), t.theme.text.highContrast;
          }.bind(void 0),
          function (t) {
            return (
              Object(r.a)(this, void 0),
              !t.bookmarkable && Object(C.css)(["margin:0 0 24px;"])
            );
          }.bind(void 0),
          function (t) {
            Object(r.a)(this, void 0);
            var e = t.mask,
              n = t.theme;
            return (
              e &&
              Object(C.css)(
                ["background-color:", ";color:", ";"],
                n.background.lightGray,
                n.text.onOverlayHighContrastDisabled
              )
            );
          }.bind(void 0),
          function (t) {
            Object(r.a)(this, void 0);
            var e = t.blur,
              n = t.theme;
            return e && Object(C.css)(["color:", ";"], n.colors.text5);
          }.bind(void 0),
          Z,
          function (t) {
            return Object(r.a)(this, void 0), t.mask ? "14px" : "16px";
          }.bind(void 0),
          function (t) {
            return Object(r.a)(this, void 0), t.mask ? "20px" : "24px";
          }.bind(void 0),
          $,
          Q,
          function (t) {
            Object(r.a)(this, void 0);
            var e = t.mask,
              n = t.theme;
            return e ? n.background.overlayFade : n.background.highContrastFade;
          }.bind(void 0),
          function (t) {
            Object(r.a)(this, void 0);
            var e = t.mask,
              n = t.theme;
            return e
              ? n.background.overlayFadeHover
              : n.background.highContrastFadeHover;
          }.bind(void 0)
        ),
        tt = C.default.div.withConfig({ componentId: "sc-3xfm45-9" })(
          [
            "position:absolute;height:100%;width:100%;top:0;left:0;background:",
            ";",
          ],
          function (t) {
            return (
              Object(r.a)(this, void 0), t.theme.background.blurThumbnailShade
            );
          }.bind(void 0)
        );
    },
    1326: function (t, e, n) {
      "use strict";
      n(31), n(54);
      var i = n(1),
        r = n(34),
        o = n(37),
        c = n(36),
        a = n(45),
        s = n(30),
        l = n(0),
        u = n(6),
        d = n.n(u),
        b = n(51),
        h = n(5),
        f = (n(65), n(11)),
        j = n(2);
      function p(t) {
        var e = t.item,
          n = Object(f.b)(e.url, { utmContent: "work-new_items" });
        return Object(l.jsxs)(O, {
          children: [
            Object(l.jsx)(m, {
              href: n,
              target: "_blank",
              rel: "noopener",
              className: "gtm-work-aside-booth",
              children: Object(l.jsx)(g, {
                style: {
                  backgroundImage: Object(f.d)(e.primary_image.f_150x150.url),
                },
              }),
            }),
            Object(l.jsxs)(v, {
              children: [
                Object(l.jsx)(x, { children: e.category.name }),
                Object(l.jsx)(y, {
                  children: Object(l.jsx)(w, {
                    href: n,
                    target: "_blank",
                    rel: "noopener",
                    className: "gtm-work-aside-booth",
                    children: e.name,
                  }),
                }),
                null !== e.price_str &&
                  Object(l.jsx)(k, { children: e.price_str }),
              ],
            }),
          ],
        });
      }
      var O = j.default.li.withConfig({ componentId: "sc-1ha8wra-0" })(
          ["display:flex;border-bottom:1px solid ", ";padding:10px 0;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.border;
          }.bind(void 0)
        ),
        m = j.default.a.withConfig({ componentId: "sc-1ha8wra-1" })([
          "text-decoration:none;",
        ]),
        v = j.default.div.withConfig({ componentId: "sc-1ha8wra-2" })([
          "display:flex;flex-flow:column;overflow:hidden;margin-left:8px;",
        ]),
        g = j.default.div.withConfig({ componentId: "sc-1ha8wra-3" })([
          "width:80px;height:80px;border-radius:8px;background-color:#fff;background-size:cover;background-repeat:no-repeat;background-position:center;",
        ]),
        x = j.default.span.withConfig({ componentId: "sc-1ha8wra-4" })(
          ["margin-bottom:4px;color:", ";font-size:12px;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text3;
          }.bind(void 0)
        ),
        y = j.default.div.withConfig({ componentId: "sc-1ha8wra-5" })([
          "margin-bottom:8px;font-size:12px;font-weight:bold;line-height:1;",
        ]),
        w = j.default.a.withConfig({ componentId: "sc-1ha8wra-6" })(
          ["color:", ";text-decoration:none;font-size:12px;line-height:16px;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        k = j.default.span.withConfig({ componentId: "sc-1ha8wra-7" })(
          ["font-weight:bold;color:", ";font-size:12px;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        C = n(26),
        I = n(268),
        _ = n(870);
      function S(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(s.a)(t);
          if (e) {
            var r = Object(s.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(a.a)(this, n);
        };
      }
      var P = { fetchBoothShopItems: I.f },
        D = (function (t) {
          Object(c.a)(n, t);
          var e = S(n);
          function n() {
            return Object(r.a)(this, n), e.apply(this, arguments);
          }
          return (
            Object(o.a)(n, [
              {
                key: "componentDidMount",
                value: function () {
                  this.props.boothShopItems ||
                    this.fetchData(this.props.userId);
                },
              },
              {
                key: "componentDidUpdate",
                value: function (t) {
                  this.props.boothShopItems ||
                    this.props.userId === t.userId ||
                    this.fetchData(this.props.userId);
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this;
                  if (
                    !this.props.boothShopItems ||
                    0 === this.props.boothShopItems.items.length
                  )
                    return null;
                  var e = this.props.boothShopItems,
                    n = e.items.slice(0, 2);
                  return Object(l.jsxs)(_.a, {
                    children: [
                      Object(l.jsx)(T, {
                        children: Object(l.jsx)(R, {
                          href: Object(f.b)(e.url, {
                            utmContent: "work-new_items",
                          }),
                          target: "_blank",
                          rel: "noopener",
                          className: "gtm-work-aside-booth",
                          children: Object(l.jsx)(h.d, {
                            children: function (e) {
                              return (
                                Object(i.a)(this, t),
                                e("%(name)�췇OOTH", {
                                  name: this.props.userName,
                                })
                              );
                            }.bind(this),
                          }),
                        }),
                      }),
                      Object(l.jsx)(z, { children: e.account_name }),
                      Object(l.jsx)(N, {
                        children: n.map(
                          function (e) {
                            return (
                              Object(i.a)(this, t),
                              Object(l.jsx)(p, { item: e }, e.id)
                            );
                          }.bind(this)
                        ),
                      }),
                    ],
                  });
                },
              },
              {
                key: "fetchData",
                value: function (t) {
                  var e = this.props,
                    n = e.isSelf;
                  (0, e.fetchBoothShopItems)(t, n, 2);
                },
              },
            ]),
            n
          );
        })(d.a.Component),
        T =
          ((e.a = Object(b.connect)(function (t, e) {
            var n = e.userId;
            return { isSelf: n === Object(C.k)(t), boothShopItems: I.b(t, n) };
          }, P)(D)),
          j.default.div.withConfig({ componentId: "alwh9r-0" })([
            "margin-bottom:4px;line-height:1;font-size:12px;",
          ])),
        R = j.default.a.withConfig({ componentId: "alwh9r-1" })(
          ["color:", ";text-decoration:none;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text3;
          }.bind(void 0)
        ),
        z = j.default.h3.withConfig({ componentId: "alwh9r-2" })(
          ["margin:0;line-height:1;font-size:1em;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        N = j.default.ul.withConfig({ componentId: "alwh9r-3" })([
          "padding:0;margin:0;list-style:none;",
        ]);
    },
    1327: function (t, e, n) {
      t.exports = {
        listContainer: "_24gUVHP",
        list: "_1itUmjH",
        question: "_2PxgB-f",
        checkIconContainer: "_2rnNiT8",
        item: "_38nwiDr",
        choiceText: "KP4DpcB",
      };
    },
    1343: function (t, e, n) {
      "use strict";
      n.d(e, "b", function () {
        return ee;
      }),
        n.d(e, "a", function () {
          return ne;
        });
      n(53), n(164);
      var i = n(8),
        r = n(4),
        o = n(1),
        c = n(0),
        a = n(89),
        s = n(9),
        l = n(6),
        u = n.n(l),
        d = n(608),
        b = n(41),
        h = n(390),
        f = n(347),
        j = n(1153),
        p = n(414),
        O = n(15),
        m = n(3),
        v = n(26),
        g = n(95),
        x = n(2),
        y = n(1344),
        w = (n(31), n(288)),
        k = n(301),
        C = n(192);
      function I(t) {
        var e = this,
          n = t.isExpand,
          i = t.onExpandClick,
          r = Object(O.c)(p.e),
          a = Object(O.c)(
            function (t) {
              return Object(o.a)(this, e), Object(C.c)(t, "adsHide");
            }.bind(this)
          ),
          s = Object(f.d)(r && !a, null, {
            from: { height: "0px", opacity: 0 },
            enter: { height: "540px", opacity: 1 },
            config: w.a,
          });
        return a
          ? null
          : (r && n && i && i(),
            Object(c.jsx)(c.Fragment, {
              children: s.map(
                function (t) {
                  Object(o.a)(this, e);
                  var n = t.item,
                    i = t.props,
                    r = t.key;
                  return (
                    n &&
                    Object(c.jsx)(
                      _,
                      {
                        style: i,
                        children: Object(c.jsx)(k.c, { kind: k.a["500x500"] }),
                      },
                      r
                    )
                  );
                }.bind(this)
              ),
            }));
      }
      var _ = Object(x.default)(h.a.div).withConfig({
          componentId: "sc-185y4-0",
        })(
          [
            "display:flex;flex-flow:column;align-items:center;justify-content:flex-end;background:",
            ";padding:16px 0 0;overflow:hidden;opacity:0;&:empty{padding:0;}",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.materials.surface7;
          }.bind(void 0)
        ),
        S = n(51),
        P = n(11),
        D = n(268);
      function T(t) {
        return Object(c.jsx)(R, {
          viewBox: "0 0 24 24",
          inherit: t.inherit,
          children: Object(c.jsx)("path", {
            d:
              "M8 6C8 3.79086 9.79086 2 12 2C14.2091 2 16 3.79086 16 6H17C18.6569 6 20 7.34315 20\n 9V18C20 19.6569 18.6569 21 17 21H7C5.34315 21 4 19.6569 4 18V9C4 7.34315 5.34315 6 7 6H8ZM10 6C10\n 4.89543 10.8954 4 12 4C13.1046 4 14 4.89543 14 6H10ZM8 8V10C8 10.5523 8.44772 11 9 11C9.55228 11\n 10 10.5523 10 10V8H14V10C14 10.5523 14.4477 11 15 11C15.5523 11 16 10.5523 16 10V8H17C17.5523 8 18\n 8.44772 18 9V18C18 18.5523 17.5523 19 17 19H7C6.44772 19 6 18.5523 6 18V9C6 8.44772 6.44772 8 7 8H8Z",
            fillRule: "evenodd",
            clipRule: "evenodd",
          }),
        });
      }
      var R = x.default.svg.withConfig({ componentId: "sc-1iiguxo-0" })(
          [
            "stroke:none;fill:",
            ";width:24px;height:24px;line-height:0;font-size:0;vertical-align:middle;",
          ],
          function (t) {
            return (
              Object(o.a)(this, void 0),
              t.inherit ? "currentColor" : t.theme.text.default
            );
          }.bind(void 0)
        ),
        z = (n(65), n(5)),
        N = n(20),
        M = n.p + "d3faa2bcb20a99e9145e6fb666f2eb06.svg",
        q = n.p + "3a468b616eb52cafbc643a6d35aea376.svg";
      var U = Object(S.connect)(function (t, e) {
          var n = e.id;
          return { boothWidget: D.c(t, n), xRestrict: Object(v.l)(t) };
        })(function (t) {
          var e = t.boothWidget,
            n = t.xRestrict,
            r = Object(z.o)(),
            o = Object(i.a)(r, 1)[0];
          return e
            ? Object(c.jsxs)(E, {
                href: Object(P.b)(e.url, { utmContent: "work-mentioned_item" }),
                target: "_blank",
                rel: "noopener",
                children: [
                  Object(c.jsx)(L, {
                    children:
                      e.adult && n === N.k.Safe
                        ? Object(c.jsx)(B, {})
                        : Object(c.jsx)(F, {
                            src: e.primary_image.c_72x72.url,
                          }),
                  }),
                  Object(c.jsxs)(W, {
                    children: [
                      Object(c.jsxs)(H, {
                        children: [
                          e.adult && Object(c.jsx)(V, { children: "R-18" }),
                          Object(c.jsx)(G, { children: e.category.name }),
                        ],
                      }),
                      Object(c.jsx)(Z, { children: e.name }),
                      Object(c.jsxs)(Y, {
                        children: [
                          null !== e.price_str &&
                            Object(c.jsx)(X, { children: e.price_str }),
                          Object(c.jsxs)($, {
                            children: [o("BOOTH�㎬껭��"), Object(c.jsx)(K, {})],
                          }),
                        ],
                      }),
                    ],
                  }),
                ],
              })
            : null;
        }),
        E = x.default.a.withConfig({ componentId: "sc-13iz6hf-0" })([
          "display:flex;align-items:center;text-decoration:none;",
        ]),
        L = x.default.div.withConfig({ componentId: "sc-13iz6hf-1" })(
          [
            "position:relative;line-height:0;&::after{content:'';position:absolute;top:0;bottom:0;left:0;right:0;background-color:",
            ";}",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.materials.surface4;
          }.bind(void 0)
        ),
        A = Object(x.css)(["flex:none;width:90px;height:90px;"]),
        F = x.default.img.withConfig({ componentId: "sc-13iz6hf-2" })(
          ["", ""],
          A
        ),
        B = x.default.div.withConfig({ componentId: "sc-13iz6hf-3" })(
          [
            "",
            " background-repeat:no-repeat;background-position:center;background-image:",
            ";",
          ],
          A,
          Object(P.d)(M)
        ),
        W = x.default.div.withConfig({ componentId: "sc-13iz6hf-4" })([
          "padding:12px 16px;",
        ]),
        H = x.default.div.withConfig({ componentId: "sc-13iz6hf-5" })([
          "display:flex;line-height:22px;font-size:14px;",
        ]),
        V = x.default.span.withConfig({ componentId: "sc-13iz6hf-6" })(
          ["margin-right:8px;font-weight:bold;color:", ";"],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.semantic.r18;
          }.bind(void 0)
        ),
        G = x.default.span.withConfig({ componentId: "sc-13iz6hf-7" })(
          ["color:", ";"],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.text.noteLabel;
          }.bind(void 0)
        ),
        Z = x.default.p.withConfig({ componentId: "sc-13iz6hf-8" })(
          [
            "margin:0;max-width:224px;line-height:22px;font-size:14px;font-weight:bold;color:",
            ";white-space:nowrap;overflow:hidden;text-overflow:ellipsis;",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.materials.text5;
          }.bind(void 0)
        ),
        Y = x.default.div.withConfig({ componentId: "sc-13iz6hf-9" })([
          "display:flex;justify-content:space-between;white-space:nowrap;",
        ]),
        X = x.default.span.withConfig({ componentId: "sc-13iz6hf-10" })(
          [
            "margin-right:24px;line-height:22px;font-size:14px;font-weight:bold;color:",
            ";",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.materials.text5;
          }.bind(void 0)
        ),
        $ = x.default.span.withConfig({ componentId: "sc-13iz6hf-11" })(
          [
            "display:inline-flex;align-items:center;margin-right:4px;font-size:14px;line-height:22px;color:",
            ";",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.brand.booth;
          }.bind(void 0)
        ),
        K = x.default.div.withConfig({ componentId: "sc-13iz6hf-12" })(
          [
            "width:8px;height:10px;background-repeat:no-repeat;background-position:center;background-image:",
            ";",
          ],
          Object(P.d)(q)
        );
      var Q = { fetchBoothWidget: D.g };
      var J = Object(S.connect)(function (t, e) {
          var n = e.workType,
            i = e.id,
            r = Object(P.e)(Object(D.d)(t, n, i));
          return {
            descriptionBoothId: r,
            boothWidget: null !== r ? Object(D.c)(t, r) : void 0,
          };
        }, Q)(function (t) {
          var e = this,
            n = t.expanded,
            r = t.descriptionBoothId,
            l = t.boothWidget,
            u = t.fetchBoothWidget,
            d = Object(s.i)(!1),
            b = Object(s.i)(!1),
            h = Object(s.j)(!1),
            f = Object(i.a)(h, 2),
            j = f[0],
            p = f[1];
          Object(s.d)(
            function () {
              Object(o.a)(this, e), null !== r && u(r);
            }.bind(this),
            [r, u]
          );
          var O = Object(s.a)(
              function () {
                var t = this;
                Object(o.a)(this, e),
                  (d.current = !0),
                  setTimeout(
                    function () {
                      Object(o.a)(this, t), (b.current = !1), p(!0);
                    }.bind(this),
                    200
                  );
              }.bind(this),
              [p, d]
            ),
            m = Object(s.a)(
              function () {
                var t = this;
                Object(o.a)(this, e),
                  (d.current = !1),
                  setTimeout(
                    function () {
                      Object(o.a)(this, t), d.current || p(!1);
                    }.bind(this),
                    200
                  );
              }.bind(this),
              [p, d]
            ),
            v = Object(s.a)(
              function (t) {
                var n = this;
                Object(o.a)(this, e),
                  t.target === document &&
                    j &&
                    setTimeout(
                      function () {
                        Object(o.a)(this, n),
                          d.current || ((b.current = !0), p(!1));
                      }.bind(this),
                      800
                    );
              }.bind(this),
              [j]
            );
          return (
            Object(s.d)(
              function () {
                var t = this;
                Object(o.a)(this, e);
                var n = !a.g() || { capture: !0, passive: !0 };
                return (
                  window.addEventListener("scroll", v, n),
                  function () {
                    Object(o.a)(this, t),
                      window.removeEventListener("scroll", v, n);
                  }.bind(this)
                );
              }.bind(this),
              [v]
            ),
            Object(s.d)(
              function () {
                Object(o.a)(this, e), l && ((b.current = !1), p(!0));
              }.bind(this),
              [l]
            ),
            null !== r && l
              ? Object(c.jsxs)(tt, {
                  children: [
                    Object(c.jsx)(it, {
                      expanded: n,
                      children: Object(c.jsx)(rt, {
                        href: Object(P.b)(l.url, {
                          utmContent: "work-mentioned_item",
                        }),
                        target: "_blank",
                        rel: "noopener",
                        onMouseOver: O,
                        onMouseLeave: m,
                        children: Object(c.jsx)(T, { inherit: !0 }),
                      }),
                    }),
                    Object(c.jsx)(et, {
                      show: j,
                      slow: b.current,
                      onMouseOver: O,
                      onMouseLeave: m,
                      children: Object(c.jsx)(nt, {
                        children: Object(c.jsx)(U, { id: r }),
                      }),
                    }),
                  ],
                })
              : null
          );
        }),
        tt = x.default.div.withConfig({ componentId: "n8lc4e-0" })([
          "position:absolute;bottom:0;left:0;right:0;z-index:1;display:flex;flex-flow:column-reverse;align-items:flex-end;pointer-events:none;",
        ]),
        et = x.default.div.withConfig({ componentId: "n8lc4e-1" })(
          [
            "margin-right:8px;margin-bottom:4px;opacity:0;visibility:hidden;pointer-events:all;transition:",
            " visibility,",
            " opacity;",
            ";",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.slow ? "1.6s" : "0.2s";
          }.bind(void 0),
          function (t) {
            return Object(o.a)(this, void 0), t.slow ? "1.6s" : "0.2s";
          }.bind(void 0),
          function (t) {
            return (
              Object(o.a)(this, void 0),
              t.show && Object(x.css)(["opacity:1;visibility:visible;"])
            );
          }.bind(void 0)
        ),
        nt = x.default.div.withConfig({ componentId: "n8lc4e-2" })(
          ["border-radius:8px;background-color:", ";overflow:hidden;"],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.materials.surface8;
          }.bind(void 0)
        ),
        it = x.default.div.withConfig({ componentId: "n8lc4e-3" })(
          ["padding-right:8px;padding-bottom:8px;pointer-events:all;", ""],
          function (t) {
            return (
              Object(o.a)(this, void 0),
              t.expanded &&
                Object(x.css)([
                  "box-sizing:border-box;display:flex;justify-content:flex-end;width:100%;",
                ])
            );
          }.bind(void 0)
        ),
        rt = x.default.a.withConfig({ componentId: "n8lc4e-4" })(
          [
            "display:flex;align-items:center;justify-content:center;width:40px;height:40px;border-radius:50%;background-color:",
            ";color:",
            ";",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.materials.surface8;
          }.bind(void 0),
          function (t) {
            return Object(o.a)(this, void 0), t.theme.materials.text5;
          }.bind(void 0)
        ),
        ot = (n(176), n(297)),
        ct = u.a.memo(function (t) {
          var e = this,
            n = t.busy,
            i = t.onClick,
            r = t.shortcutVk,
            a = t.isUnlisted,
            l = Object(s.i)(null),
            u = Object(s.a)(
              function (t) {
                Object(o.a)(this, e), t.preventDefault(), l.current.click();
              }.bind(this),
              [l]
            );
          return Object(c.jsxs)(c.Fragment, {
            children: [
              Object(c.jsxs)(st, {
                type: "button",
                disabled: n,
                onClick: i,
                ref: l,
                isUnlisted: a,
                children: [
                  Object(c.jsx)(lt, {}),
                  Object(c.jsxs)(ut, {
                    children: [
                      Object(c.jsx)(at, {}),
                      Object(c.jsx)(ot.default, {
                        vk: ["ArrowDown", "j"],
                        onShortcut: u,
                      }),
                    ],
                  }),
                ],
              }),
              void 0 !== r &&
                Object(c.jsx)(ot.default, { vk: r, onShortcut: u }),
            ],
          });
        });
      function at() {
        var t = Object(z.p)(z.m.action.ns),
          e = Object(i.a)(t, 1)[0];
        return Object(c.jsxs)(c.Fragment, {
          children: [e(z.m.action.link.see_all_multiple_image), " "],
        });
      }
      var st = x.default.button.withConfig({ componentId: "emr523-0" })(
          [
            "display:flex;justify-content:center;position:absolute;bottom:",
            "px;left:0;right:0;width:100%;padding:64px 0 8px;border:none;outline:none;cursor:pointer;background-color:transparent;&:disabled{opacity:0.32;cursor:default;}",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.isUnlisted ? 0 : 48;
          }.bind(void 0)
        ),
        lt = x.default.div.withConfig({ componentId: "emr523-1" })(
          [
            "position:absolute;z-index:-1;top:0;right:0;bottom:0;left:0;background-color:",
            ";mask-image:linear-gradient(to bottom,transparent,#000 100%,#000 0);",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.background.overlayBar;
          }.bind(void 0)
        ),
        ut = x.default.div.withConfig({ componentId: "emr523-2" })(
          [
            "display:flex;justify-content:center;align-items:center;box-sizing:border-box;margin:0 auto;height:40px;min-width:184px;line-height:22px;font-size:14px;font-weight:bold;border-radius:20px;padding:0 24px;background:",
            ";color:",
            ";",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.background.highContrast;
          }.bind(void 0),
          function (t) {
            return Object(o.a)(this, void 0), t.theme.text.highContrast;
          }.bind(void 0)
        ),
        dt = n(1287),
        bt = n(82);
      function ht(t) {
        var e = t.currentPage,
          n = t.theme.writingMode,
          i = t.outline,
          r = t.onChangePage;
        return i.length <= 1 || n === bt.e.Vertical
          ? null
          : Object(c.jsx)(ft, {
              children: Object(c.jsx)(dt.b, {
                page: e,
                pageCount: i.length,
                onChange: r,
              }),
            });
      }
      var ft = x.default.div.withConfig({ componentId: "sc-15rcvt5-0" })([
          "display:flex;justify-content:center;padding:8px;margin-bottom:-16px;",
        ]),
        jt = (n(640), n(255), n(27)),
        pt = n(34),
        Ot = n(37),
        mt = n(36),
        vt = n(45),
        gt = n(30),
        xt = n(1282),
        yt = n(605),
        wt = n(1054),
        kt = n(689),
        Ct = n(1290);
      function It(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function _t(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? It(Object(n), !0).forEach(function (e) {
                Object(r.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : It(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function St(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(gt.a)(t);
          if (e) {
            var r = Object(gt.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(vt.a)(this, n);
        };
      }
      function Pt(t) {
        var e = this,
          n = t.marked,
          r = t.outline,
          a = t.theme,
          l = t.currentPage,
          d = t.enableGlossaryLink,
          b = t.forPreview,
          h = t.onGlossaryLinkChange,
          f = t.onWritingModeChange,
          j = t.onFontChange,
          p = t.onFontSizeChange,
          m = t.onColorChange,
          g = t.onMarkerClick,
          x = Object(z.p)([z.m.common.ns, z.m.novel.ns]),
          y = Object(i.a)(x, 1)[0],
          w = Object(z.o)(),
          k = Object(i.a)(w, 1)[0],
          C = Object(O.c)(v.r),
          I = Object(s.a)(
            function (t) {
              var n = this;
              return (
                Object(o.a)(this, e),
                function () {
                  return Object(o.a)(this, n), f(t);
                }.bind(this)
              );
            }.bind(this),
            [f]
          ),
          _ = Object(s.a)(
            function (t) {
              var n = this;
              return (
                Object(o.a)(this, e),
                function () {
                  return Object(o.a)(this, n), j(t);
                }.bind(this)
              );
            }.bind(this),
            [j]
          ),
          S = Object(s.a)(
            function (t) {
              var n = this;
              return (
                Object(o.a)(this, e),
                function () {
                  return Object(o.a)(this, n), p(t);
                }.bind(this)
              );
            }.bind(this),
            [p]
          ),
          P = Object(s.a)(
            function () {
              return (
                Object(o.a)(this, e),
                Object(c.jsxs)(kt.a, {
                  children: [
                    Object(c.jsx)(Dt, { current: a.color, onColorChange: m }),
                    Object(c.jsx)(kt.e, { children: k("�뉐춻�듐궎��") }),
                    Object(c.jsx)(kt.b, {
                      onClick: S(bt.c.Small),
                      icon: Object(c.jsx)(Wt, {
                        marked: a.fontSize === bt.c.Small,
                      }),
                      children: k("弱�"),
                    }),
                    Object(c.jsx)(kt.b, {
                      onClick: S(bt.c.Medium),
                      icon: Object(c.jsx)(Wt, {
                        marked: a.fontSize === bt.c.Medium,
                      }),
                      children: k("訝�"),
                    }),
                    Object(c.jsx)(kt.b, {
                      onClick: S(bt.c.Big),
                      icon: Object(c.jsx)(Wt, {
                        marked: a.fontSize === bt.c.Big,
                      }),
                      children: k("鸚�"),
                    }),
                    Object(c.jsx)(kt.e, { children: k("�뺛궔�녈깉") }),
                    Object(c.jsx)(kt.b, {
                      onClick: _(bt.b.Gothic),
                      icon: Object(c.jsx)(Wt, {
                        marked: a.font === bt.b.Gothic,
                      }),
                      children: k("�담궥�껁궚"),
                    }),
                    Object(c.jsx)(kt.b, {
                      onClick: _(bt.b.Mincho),
                      icon: Object(c.jsx)(Wt, {
                        marked: a.font === bt.b.Mincho,
                      }),
                      children: k("�롦쐻"),
                    }),
                    Object(c.jsx)(kt.e, { children: k("�볝깷�쇈궋��") }),
                    Object(c.jsx)(kt.b, {
                      onClick: I(bt.e.Horizontal),
                      icon: Object(c.jsx)(Wt, {
                        marked: a.writingMode === bt.e.Horizontal,
                      }),
                      children: k("與ゆ쎑��"),
                    }),
                    Object(c.jsx)(kt.b, {
                      onClick: I(bt.e.Vertical),
                      icon: Object(c.jsx)(Wt, {
                        marked: a.writingMode === bt.e.Vertical,
                      }),
                      children: k("潁�쎑��"),
                    }),
                    C &&
                      Object(c.jsxs)(c.Fragment, {
                        children: [
                          Object(c.jsx)(kt.e, {
                            children: y(z.m.common.sonota),
                          }),
                          Object(c.jsx)(kt.f, {
                            current: d,
                            onClick: h,
                            children: y(
                              z.m.novel.series.glossary.setteishiryounorinku
                            ),
                          }),
                        ],
                      }),
                  ],
                })
              );
            }.bind(this),
            [
              a.color,
              a.fontSize,
              a.font,
              a.writingMode,
              m,
              y,
              k,
              S,
              _,
              I,
              C,
              d,
              h,
            ]
          ),
          D = Object(s.a)(
            function (t) {
              var n = this;
              return (
                Object(o.a)(this, e),
                Object(c.jsx)(kt.a, {
                  onClick: t,
                  children: r.map(
                    function (t) {
                      var e = this;
                      return (
                        Object(o.a)(this, n),
                        Object(c.jsxs)(
                          u.a.Fragment,
                          {
                            children: [
                              1 !== r.length &&
                                Object(c.jsx)(kt.c, {
                                  href: "#".concat(t.page),
                                  current: l === t.page,
                                  children: t.page,
                                }),
                              t.chapters.map(
                                function (n) {
                                  return (
                                    Object(o.a)(this, e),
                                    Object(c.jsx)(
                                      kt.c,
                                      {
                                        href: "#".concat(
                                          Object(Ct.a)({
                                            pageNumber: t.page,
                                            chapterNumber: n.number,
                                          })
                                        ),
                                        title: n.title,
                                        children: n.title,
                                      },
                                      n.number
                                    )
                                  );
                                }.bind(this)
                              ),
                            ],
                          },
                          t.page
                        )
                      );
                    }.bind(this)
                  ),
                })
              );
            }.bind(this),
            [l, r]
          );
        return Object(c.jsxs)(Mt, {
          children: [
            (1 !== r.length || 0 !== r[0].chapters.length) &&
              Object(c.jsx)(qt, {
                children: Object(c.jsx)(wt.b, {
                  render: D,
                  transparent: !0,
                  closePopupOnScroll: !0,
                  children: Object(c.jsx)(Lt, {}),
                }),
              }),
            Object(c.jsx)(qt, {
              children: Object(c.jsx)(wt.b, {
                render: P,
                transparent: !0,
                closePopupOnScroll: !0,
                children: Object(c.jsx)(At, {}),
              }),
            }),
            !b && Object(c.jsx)(Ut, { marked: n, onClick: g }),
          ],
        });
      }
      var Dt = (function (t) {
        Object(mt.a)(n, t);
        var e = St(n);
        function n() {
          return Object(pt.a)(this, n), e.apply(this, arguments);
        }
        return (
          Object(Ot.a)(n, [
            {
              key: "render",
              value: function () {
                var t = this;
                return Object(c.jsx)(zt, {
                  children: Object.values(bt.a).map(
                    function (e) {
                      return (
                        Object(o.a)(this, t),
                        Object(c.jsx)(
                          Tt,
                          {
                            onClick: this.makeColorHandler(e),
                            color: e,
                            selected: e === this.props.current,
                          },
                          e
                        )
                      );
                    }.bind(this)
                  ),
                });
              },
            },
            {
              key: "makeColorHandler",
              value: function (t) {
                var e = this;
                return function () {
                  Object(o.a)(this, e), (0, this.props.onColorChange)(t);
                }.bind(this);
              },
            },
          ]),
          n
        );
      })(u.a.PureComponent);
      function Tt(t) {
        return Object(c.jsx)("li", {
          children: Object(c.jsx)(Nt, {
            onClick: t.onClick,
            fill: bt.g[t.color],
            selected: t.selected,
          }),
        });
      }
      function Rt(t) {
        return Object(c.jsx)("svg", {
          width: 32,
          height: 32,
          viewBox: "0 0 32 32",
          className: t.className,
          children: Object(c.jsx)("circle", {
            cx: 16,
            cy: 16,
            r: 15,
            onClick: t.onClick,
          }),
        });
      }
      var zt = x.default.ul.withConfig({ componentId: "za4ngn-0" })([
          "list-style:none;display:flex;margin:8px;padding:0;> li{justify-content:center;margin-right:8px;:last-child{margin-right:0;}}",
        ]),
        Nt = Object(x.default)(Rt).withConfig({ componentId: "za4ngn-1" })(
          [
            "> circle{cursor:pointer;stroke-width:2;stroke-opacity:",
            ";stroke:",
            ";fill:",
            ";&:hover{stroke-opacity:1;stroke:",
            ";}}",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.selected ? 0.8 : 0.08;
          }.bind(void 0),
          function (t) {
            Object(o.a)(this, void 0);
            var e = t.selected,
              n = t.theme;
            return e ? n.brand.pixiv : n.background.clearContrast;
          }.bind(void 0),
          function (t) {
            return Object(o.a)(this, void 0), t.fill;
          }.bind(void 0),
          function (t) {
            return Object(o.a)(this, void 0), t.theme.brand.pixiv;
          }.bind(void 0)
        ),
        Mt = x.default.div.withConfig({ componentId: "za4ngn-2" })([
          "display:flex;margin-right:auto;padding:8px 16px;> *{margin-right:16px;&:last-child{margin-right:0;}}",
        ]),
        qt = x.default.div.withConfig({ componentId: "za4ngn-3" })([
          "position:relative;",
        ]);
      function Ut(t) {
        t.marked;
        var e = Object(jt.a)(t, ["marked"]);
        return Object(c.jsx)(
          Et,
          _t(
            _t({}, e),
            {},
            {
              type: "button",
              children: Object(c.jsx)(yt.a, { marked: t.marked }),
            }
          )
        );
      }
      var Et = x.default.button.withConfig({ componentId: "za4ngn-4" })(
          [
            "display:inline-block;box-sizing:content-box;padding:0;color:inherit;background:none;border:none;line-height:1;width:32px;height:32px;cursor:pointer;&:disabled{opacity:",
            ";cursor:default;}",
          ],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.opacity.disabled;
          }.bind(void 0)
        ),
        Lt = Object(x.default)(Ft).withConfig({ componentId: "za4ngn-5" })([
          "> path{stroke:none;fill:currentColor;}",
        ]),
        At = Object(x.default)(Bt).withConfig({ componentId: "za4ngn-6" })([
          "> path{stroke:none;fill:currentColor;}",
        ]);
      function Ft(t) {
        return Object(c.jsx)("svg", {
          width: 20,
          height: 16,
          viewBox: "0 0 20 16",
          className: t.className,
          children: Object(c.jsx)("path", {
            d:
              "\nM1.5 3.5C2.32843 3.5 3 2.82837 3 2C3 1.17163 2.32843 0.5 1.5 0.5C0.67157 0.5 0 1.17163 0 2\nC0 2.82837 0.67157 3.5 1.5 3.5ZM5 2C5 1.44775 5.44769 1 6 1H19C19.5523 1 20 1.44775 20 2\nC20 2.55225 19.5523 3 19 3H6C5.44769 3 5 2.55225 5 2ZM6 7C5.44769 7 5 7.44775 5 8C5 8.55225 5.44769 9 6 9\nH19C19.5523 9 20 8.55225 20 8C20 7.44775 19.5523 7 19 7H6ZM6 13C5.44769 13 5 13.4478 5 14\nC5 14.5522 5.44769 15 6 15H19C19.5523 15 20 14.5522 20 14C20 13.4478 19.5523 13 19 13H6ZM3 8\nC3 8.82837 2.32843 9.5 1.5 9.5C0.67157 9.5 0 8.82837 0 8C0 7.17163 0.67157 6.5 1.5 6.5\nC2.32843 6.5 3 7.17163 3 8ZM1.5 15.5C2.32843 15.5 3 14.8284 3 14C3 13.1716 2.32843 12.5 1.5 12.5\nC0.67157 12.5 0 13.1716 0 14C0 14.8284 0.67157 15.5 1.5 15.5Z\n",
          }),
        });
      }
      function Bt(t) {
        return Object(c.jsx)("svg", {
          width: 27,
          height: 19,
          viewBox: "0 0 27 19",
          className: t.className,
          children: Object(c.jsx)("path", {
            fillRule: "evenodd",
            d:
              "\nM4.00127 14L2.4265 17.8765C2.21861 18.3881 1.6353 18.6344 1.12364 18.4265\nC0.611984 18.2186 0.365646 17.6354 0.573532 17.1237L7.07353 1.12366\nC7.41136 0.292114 8.58867 0.292114 8.9265 1.12366L15.4265 17.1237\nC15.6343 17.6354 15.388 18.2186 14.8764 18.4265C14.3647 18.6344 13.7814 18.3881 13.5735 17.8765L11.9988 14\nH4.00127ZM25 8.5V8.64099C23.9932 7.77405 22.6828 7.25 21.25 7.25C18.0743 7.25 15.5 9.82434 15.5 13\nC15.5 16.1757 18.0743 18.75 21.25 18.75C22.6828 18.75 23.9932 18.226 25 17.359V17.5\nC25 18.0524 25.4477 18.5 26 18.5C26.5523 18.5 27 18.0524 27 17.5V8.5C27 7.94775 26.5523 7.5 26 7.5\nC25.4477 7.5 25 7.94775 25 8.5ZM25 13C25 15.071 23.321 16.75 21.25 16.75C19.1789 16.75 17.5 15.071 17.5 13\nC17.5 10.929 19.1789 9.25 21.25 9.25C23.321 9.25 25 10.929 25 13ZM11.1863 12H4.81377L8.00005 4.15686\nL11.1863 12Z\n",
          }),
        });
      }
      var Wt = Object(x.default)(xt.b).withConfig({ componentId: "za4ngn-7" })(
          ["stroke-opacity:1;stroke:", ";"],
          function (t) {
            return (
              Object(o.a)(this, void 0), t.marked ? t.theme.text.note : "none"
            );
          }.bind(void 0)
        ),
        Ht = (n(29), n(70), n(68));
      function Vt(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function Gt(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? Vt(Object(n), !0).forEach(function (e) {
                Object(r.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : Vt(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function Zt(t, e) {
        var n;
        if ("undefined" == typeof Symbol || null == t[Symbol.iterator]) {
          if (
            Array.isArray(t) ||
            (n = (function (t, e) {
              if (!t) return;
              if ("string" == typeof t) return Yt(t, e);
              var n = Object.prototype.toString.call(t).slice(8, -1);
              "Object" === n && t.constructor && (n = t.constructor.name);
              if ("Map" === n || "Set" === n) return Array.from(t);
              if (
                "Arguments" === n ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)
              )
                return Yt(t, e);
            })(t)) ||
            (e && t && "number" == typeof t.length)
          ) {
            n && (t = n);
            var i = 0,
              r = function () {};
            return {
              s: r,
              n: function () {
                return i >= t.length
                  ? { done: !0 }
                  : { done: !1, value: t[i++] };
              },
              e: function (t) {
                throw t;
              },
              f: r,
            };
          }
          throw new TypeError(
            "Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
          );
        }
        var o,
          c = !0,
          a = !1;
        return {
          s: function () {
            n = t[Symbol.iterator]();
          },
          n: function () {
            var t = n.next();
            return (c = t.done), t;
          },
          e: function (t) {
            (a = !0), (o = t);
          },
          f: function () {
            try {
              c || null == n.return || n.return();
            } finally {
              if (a) throw o;
            }
          },
        };
      }
      function Yt(t, e) {
        (null == e || e > t.length) && (e = t.length);
        for (var n = 0, i = new Array(e); n < e; n++) i[n] = t[n];
        return i;
      }
      var Xt,
        $t = a.i()
          ? ((Xt = Object(x.default)(h.a.div).withConfig({
              componentId: "rsntqo-0",
            })(["position:sticky;bottom:0;z-index:1;"])),
            function (t) {
              var e = this,
                n = t.onStickyChange,
                i = t.onMouseEnter,
                r = t.onMouseLeave,
                a = Object(jt.a)(t, [
                  "onStickyChange",
                  "onMouseEnter",
                  "onMouseLeave",
                ]),
                u = Object(s.i)(null);
              Object(l.useEffect)(
                function () {
                  var t = this;
                  if (
                    (Object(o.a)(this, e), void 0 !== n && null !== u.current)
                  ) {
                    var i = new IntersectionObserver(
                      function (e) {
                        Object(o.a)(this, t);
                        var i,
                          r = Zt(e);
                        try {
                          for (r.s(); !(i = r.n()).done; ) {
                            var c = i.value;
                            n(
                              c.boundingClientRect.top > 1 &&
                                c.intersectionRatio < 1
                            );
                          }
                        } catch (a) {
                          r.e(a);
                        } finally {
                          r.f();
                        }
                      }.bind(this),
                      { rootMargin: "-1px 0px", threshold: 1 }
                    );
                    return (
                      i.observe(u.current),
                      function () {
                        Object(o.a)(this, t), i.disconnect();
                      }.bind(this)
                    );
                  }
                }.bind(this),
                [n]
              );
              var d = Object(s.a)(
                  function () {
                    Object(o.a)(this, e), i && i();
                  }.bind(this),
                  [i]
                ),
                b = Object(s.a)(
                  function () {
                    Object(o.a)(this, e), r && r();
                  }.bind(this),
                  [r]
                );
              return Object(c.jsx)(
                Xt,
                Gt(
                  {
                    ref: void 0 !== n ? u : void 0,
                    onMouseEnter: d,
                    onMouseLeave: b,
                  },
                  a
                )
              );
            })
          : (function () {
              var t = this,
                e = Object(Ht.a)(
                  function () {
                    return (
                      Object(o.a)(this, t), n.e(68).then(n.bind(null, 2179))
                    );
                  }.bind(this)
                );
              return function (t) {
                return Object(c.jsx)(u.a.Suspense, {
                  fallback: null,
                  children: Object(c.jsx)(e, Gt({}, t)),
                });
              };
            })();
      function Kt(t, e) {
        var n;
        if ("undefined" == typeof Symbol || null == t[Symbol.iterator]) {
          if (
            Array.isArray(t) ||
            (n = (function (t, e) {
              if (!t) return;
              if ("string" == typeof t) return Qt(t, e);
              var n = Object.prototype.toString.call(t).slice(8, -1);
              "Object" === n && t.constructor && (n = t.constructor.name);
              if ("Map" === n || "Set" === n) return Array.from(t);
              if (
                "Arguments" === n ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)
              )
                return Qt(t, e);
            })(t)) ||
            (e && t && "number" == typeof t.length)
          ) {
            n && (t = n);
            var i = 0,
              r = function () {};
            return {
              s: r,
              n: function () {
                return i >= t.length
                  ? { done: !0 }
                  : { done: !1, value: t[i++] };
              },
              e: function (t) {
                throw t;
              },
              f: r,
            };
          }
          throw new TypeError(
            "Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
          );
        }
        var o,
          c = !0,
          a = !1;
        return {
          s: function () {
            n = t[Symbol.iterator]();
          },
          n: function () {
            var t = n.next();
            return (c = t.done), t;
          },
          e: function (t) {
            (a = !0), (o = t);
          },
          f: function () {
            try {
              c || null == n.return || n.return();
            } finally {
              if (a) throw o;
            }
          },
        };
      }
      function Qt(t, e) {
        (null == e || e > t.length) && (e = t.length);
        for (var n = 0, i = new Array(e); n < e; n++) i[n] = t[n];
        return i;
      }
      function Jt(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function te(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? Jt(Object(n), !0).forEach(function (e) {
                Object(r.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : Jt(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function ee(t) {
        var e = this,
          n = t.pageType,
          i = t.id,
          r = m.t.toWorkTypeFromPageType(n);
        return Object(c.jsx)(
          ne,
          te(
            te({}, t),
            {},
            {
              workData: Object(O.c)(
                function (t) {
                  return (
                    Object(o.a)(this, e),
                    void 0 !== r ? Object(g.c)(t, r, i) : void 0
                  );
                }.bind(this)
              ),
              isLoggedIn: Object(O.c)(v.r),
              busy: Object(O.c)(p.g),
              expanded: Object(O.c)(
                function (t) {
                  return Object(o.a)(this, e), Object(p.h)(t);
                }.bind(this)
              ),
            }
          )
        );
      }
      function ne(t) {
        var e = this,
          n = t.blur,
          r = t.busy,
          l = t.currentPage,
          u = t.expanded,
          d = t.id,
          h = t.marked,
          p = t.enableGlossaryLink,
          O = t.onGlossaryLinkChange,
          v = t.onChangePage,
          g = t.onColorChange,
          x = t.onExpandClick,
          w = t.onFontChange,
          k = t.onFontSizeChange,
          C = t.onMarkerClick,
          _ = t.onStickyChange,
          S = t.onWritingModeChange,
          P = t.outline,
          D = t.theme,
          T = t.workData,
          R = t.workTags,
          z = t.isLoggedIn,
          N = t.pageType,
          M = t.stickyActive,
          q = t.forPreview,
          U = void 0 !== q && q,
          E = t.onlyOnePage,
          L = !(void 0 !== E && E) && u,
          A = Object(b.g)(),
          F = Object(s.j)(!1),
          B = Object(i.a)(F, 2),
          W = B[0],
          H = B[1],
          V = Object(s.g)(
            function () {
              return (
                Object(o.a)(this, e),
                !n &&
                  (N === m.j.Illust || N === m.j.IllustUnlisted) &&
                  void 0 !== T &&
                  T.pageCount > 1 &&
                  (!L || r)
              );
            }.bind(this),
            [n, N, T, L, r]
          ),
          G = Object(s.a)(
            function () {
              Object(o.a)(this, e), L && H(!0);
            }.bind(this),
            [H, L]
          ),
          Z = Object(s.a)(
            function () {
              Object(o.a)(this, e), L && H(!1);
            }.bind(this),
            [H, L]
          ),
          Y = Object(s.i)(null),
          X = Object(s.i)(null),
          $ = (function (t, e, n) {
            var r = this,
              c = t.pathname,
              l = t.search,
              u = t.hash,
              d =
                !(arguments.length > 3 && void 0 !== arguments[3]) ||
                arguments[3],
              b =
                arguments.length > 4 && void 0 !== arguments[4] && arguments[4],
              h = 10,
              p = Object(s.j)(0),
              O = Object(i.a)(p, 2),
              m = O[0],
              v = O[1],
              g = Object(s.j)(!1),
              x = Object(i.a)(g, 2),
              y = x[0],
              w = x[1],
              k = Object(s.j)(!1),
              C = Object(i.a)(k, 2),
              I = C[0],
              _ = C[1],
              S = Object(s.i)(0);
            Object(s.d)(
              function () {
                Object(o.a)(this, r), d && ((S.current = window.scrollY), v(0));
              }.bind(this),
              [d, c, l, u]
            ),
              Object(s.d)(
                function () {
                  Object(o.a)(this, r), d && I && (S.current = window.scrollY);
                }.bind(this),
                [d, I]
              ),
              Object(s.d)(
                function () {
                  var t,
                    e = this;
                  if ((Object(o.a)(this, r), d))
                    if (I) {
                      if (y)
                        return (
                          window.addEventListener(
                            "scroll",
                            n,
                            !!Object(a.g)() && { passive: !0 }
                          ),
                          function () {
                            Object(o.a)(this, e),
                              void 0 !== t &&
                                Object(j.unstable_cancelCallback)(t),
                              window.removeEventListener("scroll", n);
                          }.bind(this)
                        );
                    } else v(window.scrollY < S.current ? 1 : 0);
                  else v(0);
                  function n() {
                    void 0 !== t && Object(j.unstable_cancelCallback)(t),
                      (t = Object(j.unstable_scheduleCallback)(
                        j.unstable_LowPriority,
                        i
                      ));
                  }
                  function i() {
                    t = void 0;
                    var e = S.current,
                      n = window.scrollY,
                      i = Math.abs(n - e) > h;
                    i && n > e && !b ? v(1) : i && n < e && v(0),
                      (S.current = n);
                  }
                }.bind(this),
                [I, y, d, h, b]
              ),
              Object(s.d)(
                function () {
                  var t = this;
                  Object(o.a)(this, r);
                  var i = e.current,
                    c = n.current;
                  if (d && null !== i && null !== c) {
                    var a = new IntersectionObserver(
                      function (e) {
                        Object(o.a)(this, t);
                        var n,
                          r = Kt(e);
                        try {
                          for (r.s(); !(n = r.n()).done; ) {
                            var a = n.value,
                              s =
                                a.boundingClientRect.top > 0 &&
                                a.intersectionRatio < 1;
                            a.target === i ? w(s) : a.target === c && _(s);
                          }
                        } catch (l) {
                          r.e(l);
                        } finally {
                          r.f();
                        }
                      }.bind(this),
                      { threshold: 1 }
                    );
                    return (
                      a.observe(i),
                      a.observe(c),
                      function () {
                        return Object(o.a)(this, t), a.disconnect();
                      }.bind(this)
                    );
                  }
                }.bind(this),
                [d, e, n]
              );
            var P = y && b ? 0 : m;
            Object(s.c)(P);
            var D = Object(s.i)(I);
            Object(s.d)(
              function () {
                Object(o.a)(this, r), (D.current = I);
              }.bind(this)
            );
            var T = Object(s.i)(!I);
            !I && D.current && (T.current = !0);
            return (
              Object(s.d)(
                function () {
                  Object(o.a)(this, r), (T.current = !1);
                }.bind(this)
              ),
              Object(f.c)({
                value: P,
                config: Object(s.g)(
                  function () {
                    return (
                      Object(o.a)(this, r),
                      te(te({}, f.b.stiff), {}, { clamp: !0 })
                    );
                  }.bind(this),
                  []
                ),
                reset: T.current,
              }).value
            );
          })(A, Y, X, M, W);
        if (void 0 === T) return null;
        var K = {
            transform: $.to(
              function (t) {
                return (
                  Object(o.a)(this, e),
                  t >= 1
                    ? "translateY(100%)"
                    : "translateY(".concat(100 * t, "%)")
                );
              }.bind(this)
            ),
          },
          Q = m.t.toWorkTypeFromPageType(N);
        return Object(c.jsxs)(c.Fragment, {
          children: [
            Object(c.jsx)(I, { isExpand: V, onExpandClick: x }),
            Object(c.jsx)(se, { ref: Y }),
            Object(c.jsx)(ce, { loading: r }),
            Object(c.jsx)($t, {
              onStickyChange: _,
              onMouseEnter: G,
              onMouseLeave: Z,
              children: Object(c.jsxs)(oe, {
                style: K,
                children: [
                  !U &&
                    Q &&
                    z &&
                    Object(c.jsx)(ie, {
                      children: Object(c.jsx)(J, {
                        id: d,
                        workType: Q,
                        expanded: L,
                      }),
                    }),
                  (N === m.j.Novel || N === m.j.NovelUnlisted) &&
                    Object(c.jsx)(ht, {
                      currentPage: l,
                      outline: P,
                      onChangePage: v,
                      theme: D,
                    }),
                  Object(c.jsxs)(re, {
                    children: [
                      (N === m.j.Novel || N === m.j.NovelUnlisted) &&
                        Object(c.jsx)(Pt, {
                          marked: h,
                          outline: P,
                          theme: D,
                          currentPage: l,
                          enableGlossaryLink: p,
                          forPreview: U,
                          onGlossaryLinkChange: O,
                          onFontChange: w,
                          onColorChange: g,
                          onFontSizeChange: k,
                          onWritingModeChange: S,
                          onMarkerClick: C,
                        }),
                      Object(c.jsx)(y.a, {
                        pageType: N,
                        id: d,
                        title: T.title,
                        tags: R,
                      }),
                      V &&
                        Object(c.jsx)(ct, {
                          illustId: T.id,
                          isUnlisted: N === m.j.IllustUnlisted,
                          busy: r,
                          onClick: x,
                          shortcutVk: "v",
                        }),
                    ],
                  }),
                ],
              }),
            }),
            Object(c.jsx)(se, { ref: X }),
          ],
        });
      }
      var ie = x.default.div.withConfig({ componentId: "ye57th-0" })([
          "position:relative;",
        ]),
        re = x.default.div.withConfig({ componentId: "ye57th-1" })([
          "display:flex;justify-content:flex-end;",
        ]),
        oe = Object(x.default)(h.a.div).withConfig({ componentId: "ye57th-2" })(
          ["background:", ";color:", ";"],
          function (t) {
            return Object(o.a)(this, void 0), t.theme.background.overlayBar;
          }.bind(void 0),
          function (t) {
            return Object(o.a)(this, void 0), t.theme.text.default;
          }.bind(void 0)
        );
      function ce(t) {
        return Object(c.jsx)(
          ae,
          te(te({}, t), {}, { loading: void 0 === t.loading ? 0 : +t.loading })
        );
      }
      var ae = Object(x.default)(d.LoadingBar)
        .attrs({ suppressClassNameWarning: !0 })
        .withConfig({ componentId: "ye57th-3" })(
        ["position:absolute;top:0;left:0;height:2px;background-color:", ";"],
        function (t) {
          return Object(o.a)(this, void 0), t.theme.brand.pixiv;
        }.bind(void 0)
      );
      var se = x.default.div.withConfig({ componentId: "ye57th-4" })([
        "pointer-events:none;width:1px;height:1px;margin:-1px auto 0;",
      ]);
    },
    1345: function (t, e, n) {
      "use strict";
      n.d(e, "b", function () {
        return Y;
      }),
        n.d(e, "a", function () {
          return vt;
        });
      n(117), n(150), n(28), n(111), n(94), n(31), n(58), n(176);
      var i = n(1),
        r = n(8),
        o = n(0),
        c = n(40),
        a = n(443),
        s = n(1304),
        l = n(276),
        u = n(6),
        d = n.n(u),
        b = n(114),
        h = n(484),
        f = n(1289),
        j = n(15),
        p = n(298),
        O = n(3),
        m = n(14),
        v = n(64),
        g = n(1319),
        x = n(1272),
        y = n(527),
        w = n(528),
        k = n(309),
        C = n(95),
        I = n(2),
        _ = n(5),
        S = n(11),
        P = n(4),
        D = n(51),
        T = n(26),
        R = n(400),
        z = n.p + "ec439768aee773098238b61dc6ae7bc6.svg",
        N = n.p + "0a4c9bfd385304c0ffe26bd08fab34ba.svg",
        M = n(175);
      function q(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function U(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? q(Object(n), !0).forEach(function (e) {
                Object(P.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : q(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      var E,
        L = Object(D.connect)(
          function (t) {
            return (
              Object(i.a)(this, void 0),
              {
                selfXRestrict: O.t.toXRestrict(Object(T.l)(t)),
                selfAdult: Object(T.h)(t),
              }
            );
          }.bind(void 0)
        )(function (t) {
          var e = t.work,
            n = t.selfAdult,
            i = t.selfXRestrict,
            c = t.isUnlisted,
            a = Object(_.p)(_.m.action.ns),
            s = Object(r.a)(a, 1)[0],
            l = Object(_.o)(),
            u = Object(r.a)(l, 1)[0],
            d = O.s.isXRestrictViewable(e.xRestrict, i) || n;
          return Object(o.jsxs)(B, {
            children: [
              Object(o.jsx)(A, {
                type: E.Like,
                value: e.likeCount,
                title: s(_.m.action.like.like),
                disabled: !0,
              }),
              d
                ? Object(o.jsx)(A, {
                    type: E.Bookmark,
                    href: c
                      ? void 0
                      : e.type === O.q.Illust
                      ? m.bookmark.detail({}, { illust_id: e.id })
                      : e.type === O.q.Novel
                      ? m.novel.bookmark.detail({}, { id: e.id })
                      : Object(S.v)(e.type),
                    disabled: c,
                    title: u("�뽧긿��깯�쇈궚"),
                    value: e.bookmarkCount,
                  })
                : Object(o.jsx)(A, {
                    type: E.Bookmark,
                    title: u("�뽧긿��깯�쇈궚"),
                    value: e.bookmarkCount,
                    disabled: !0,
                  }),
              Object(o.jsx)(A, {
                type: E.ViewCount,
                value: e.viewCount,
                title: u("�꿱├��"),
                disabled: !0,
              }),
            ],
          });
        });
      function A(t) {
        return Object(o.jsx)("li", { children: Object(o.jsx)(F, U({}, t)) });
      }
      function F(t) {
        var e = Object(S.y)(),
          n = Object(o.jsxs)(H, {
            disabled: !!t.disabled,
            role: "button",
            children: [
              Object(o.jsx)("dt", {
                children:
                  t.type === E.Bookmark
                    ? Object(o.jsx)(R.a, {})
                    : Object(o.jsx)("img", {
                        src:
                          t.type === E.Like
                            ? z
                            : t.type === E.ViewCount
                            ? N
                            : Object(S.v)(t.type),
                        width: t.type === E.ViewCount ? 14 : 12,
                        height: 12,
                      }),
              }),
              Object(o.jsx)("dd", { title: t.title, children: e(t.value) }),
            ],
          });
        return t.href
          ? Object(o.jsx)(V, { href: t.href, title: t.title, children: n })
          : n;
      }
      !(function (t) {
        (t[(t.Bookmark = 0)] = "Bookmark"),
          (t[(t.Like = 1)] = "Like"),
          (t[(t.ViewCount = 2)] = "ViewCount");
      })(E || (E = {}));
      var B = I.default.ul.withConfig({ componentId: "sc-1qvk3ka-0" })([
          "display:flex;align-items:center;margin:16px -4px;list-style:none;padding:0;> li{flex:none;margin:0 8px;}",
        ]),
        W = Object(I.css)(["display:flex;margin:0 -4px;"]),
        H = Object(I.default)(Object(M.e)("dl", ["disabled"])).withConfig({
          componentId: "sc-1qvk3ka-1",
        })(
          [
            "",
            " line-height:1;color:",
            ";cursor:pointer;> dt{margin:0 2px;line-height:0;white-space:nowrap;color:#ccc;}> dd{margin:0 2px;line-height:12px;color:",
            ";overflow:hidden;text-overflow:ellipsis;white-space:nowrap;font-size:12px;}",
            " ",
            "",
          ],
          W,
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0),
          function (t) {
            return (
              Object(i.a)(this, void 0),
              t.disabled && Object(I.css)(["cursor:auto;"])
            );
          }.bind(void 0),
          function (t) {
            return (
              Object(i.a)(this, void 0),
              !t.disabled && Object(I.css)(["&:hover > dd{color:#ababab;}"])
            );
          }.bind(void 0)
        ),
        V = I.default.a.withConfig({ componentId: "sc-1qvk3ka-2" })(
          ["", " text-decoration:none;", "{margin:0;}"],
          W,
          H
        ),
        G = d.a.memo(function (t) {
          var e = t.isUnlisted,
            n = t.time,
            i = t.workData,
            c = Object(_.o)(),
            a = Object(r.a)(c, 1)[0];
          return Object(o.jsxs)(o.Fragment, {
            children: [
              Object(o.jsx)(L, { isUnlisted: e, work: i }),
              Object(o.jsx)(Z, { title: a("�뺟㉮�ζ셽"), children: n }),
            ],
          });
        }),
        Z = I.default.div.withConfig({ componentId: "sc-5981ly-0" })(
          ["color:", ";font-size:12px;line-height:1;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0)
        );
      function Y(t) {
        var e = t.isUnlisted,
          n = t.isLoggedIn,
          i = t.workData,
          u = t.tags,
          d = t.contestBanners,
          b = t.contestData,
          h = t.requestData,
          j = t.tagsEditable,
          v = t.onTagEditClick,
          x = t.children,
          y = Object(_.p)([_.m.work.ns]),
          w = Object(r.a)(y, 1)[0],
          k = Object(p.a)(i),
          C = k.title,
          I = k.caption,
          S = Object(c.default)(i.createDate),
          P = ""
            .concat(C, "/")
            .concat(
              w(_.m.work.info_by_user.novel_by_username, {
                userName: i.userName,
              })
            );
        return Object(o.jsxs)(ut, {
          children: [
            i.type === O.q.Novel &&
              Object(o.jsx)(pt, {
                children: Object(o.jsx)(Ot, {
                  href: i.coverUrl,
                  targetBlank: !0,
                  children: Object(o.jsx)(mt, { src: i.coverUrl, alt: P }),
                }),
              }),
            Object(o.jsxs)(dt, {
              children: [
                null !== h &&
                  Object(o.jsx)(K, {
                    children: Object(o.jsx)(lt, {
                      children: Object(o.jsx)(f.a, { request: h }),
                    }),
                  }),
                Object(o.jsx)(K, {
                  children:
                    null !== b &&
                    Object(o.jsxs)(Q, {
                      href: b.url,
                      children: [Object(o.jsx)(J, { src: b.icon }), b.title],
                    }),
                }),
                Object(o.jsx)(K, {
                  children:
                    i.type === O.q.Novel &&
                    null !== i.seriesNav &&
                    Object(o.jsxs)(bt, {
                      to: m.novel.series({ id: i.seriesNav.seriesId }, {}),
                      children: [i.seriesNav.title, " #", i.seriesNav.order],
                    }),
                }),
                "" !== C &&
                  !["�↓죱", "Untitled", "�좈쥦", "臾댁젣"].includes(C) &&
                  Object(o.jsx)(tt, { children: C }),
                i.type === O.q.Novel &&
                  Object(o.jsx)(ht, { count: Array.from(i.content).length }),
                "" !== I &&
                  Object(o.jsx)(nt, {
                    children: Object(o.jsx)(s.a, {
                      maxRows: 10,
                      children: Object(o.jsx)(a.a, { children: I }),
                    }),
                  }),
                d.length > 0 && Object(o.jsx)($, { contestBanners: d }),
                !e &&
                  n &&
                  i.type === O.q.Illust &&
                  Object(o.jsx)(g.a, { workType: i.type, id: i.id }),
                Object(o.jsx)(X, {
                  isUnlisted: e,
                  workData: i,
                  tags: u,
                  tagsEditable: j,
                  onTagEditClick: v,
                }),
                Object(o.jsx)(G, {
                  isUnlisted: e,
                  time: Object(l.b)(S, !0),
                  workData: i,
                }),
                x,
              ],
            }),
          ],
        });
      }
      function X(t) {
        var e = t.isUnlisted,
          n = t.workData,
          i = t.tags,
          r = t.tagsEditable,
          c = t.onTagEditClick;
        return Object(o.jsx)(et, {
          children: Object(o.jsx)(x.a, {
            workData: n,
            tags: i,
            tagsEditable: !e && r,
            onTagEditClick: c,
          }),
        });
      }
      function $(t) {
        var e = this,
          n = t.contestBanners;
        return Object(o.jsx)(it, {
          children: n.map(
            function (t) {
              return (
                Object(i.a)(this, e),
                Object(o.jsxs)(
                  rt,
                  {
                    href: t.url,
                    children: [
                      Object(o.jsx)(ot, { src: t.bannerUrl, alt: t.title }),
                      t.showText &&
                        Object(o.jsxs)(ct, {
                          children: [
                            Object(o.jsx)(at, { children: t.title }),
                            Object(o.jsx)(st, { children: t.description }),
                          ],
                        }),
                    ],
                  },
                  t.url
                )
              );
            }.bind(this)
          ),
        });
      }
      var K = I.default.div.withConfig({ componentId: "sc-1u8nu73-0" })([
          "display:flex;flex-flow:wrap;",
        ]),
        Q = I.default.a.withConfig({ componentId: "sc-1u8nu73-1" })(
          [
            "display:inline-flex;margin-right:8px;align-items:center;line-height:22px;text-decoration:none;color:",
            ";&:hover{color:",
            ";}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.link;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.linkHover;
          }.bind(void 0)
        ),
        J = I.default.img.withConfig({ componentId: "sc-1u8nu73-2" })([
          "margin-right:4px;width:16px;height:16px;border-radius:3px;",
        ]),
        tt = I.default.h1.withConfig({ componentId: "sc-1u8nu73-3" })(
          [
            "margin:0 0 8px;color:",
            ";font-size:20px;line-height:24px;font-weight:bold;",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.default;
          }.bind(void 0)
        ),
        et = I.default.footer.withConfig({ componentId: "sc-1u8nu73-4" })(
          [
            "display:flex;margin:16px 0;color:",
            ";line-height:18px;> span{margin:0 4px;}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0)
        ),
        nt = I.default.div.withConfig({ componentId: "sc-1u8nu73-5" })([
          "margin:0 0 16px;word-break:break-word;a{text-decoration:none;color:#3d7699;&:hover,&:focus{color:#336481;}}",
        ]),
        it = I.default.nav.withConfig({ componentId: "sc-1u8nu73-6" })([
          "margin:8px 0;",
        ]),
        rt = I.default.a.withConfig({ componentId: "sc-1u8nu73-7" })([
          "position:relative;display:inline-block;margin:4px 0;line-height:0;text-decoration:none;",
        ]),
        ot = I.default.img.withConfig({ componentId: "sc-1u8nu73-8" })([
          "height:56px;",
        ]),
        ct = I.default.div.withConfig({ componentId: "sc-1u8nu73-9" })([
          "position:absolute;top:0;left:80px;line-height:1;color:#fff;",
        ]),
        at = I.default.span.withConfig({ componentId: "sc-1u8nu73-10" })([
          "display:block;margin:11px 0 7px;font-size:10px;",
        ]),
        st = I.default.span.withConfig({ componentId: "sc-1u8nu73-11" })([
          "font-size:16px;font-weight:bold;",
        ]),
        lt = I.default.div.withConfig({ componentId: "sc-1u8nu73-12" })([
          "padding-bottom:6px;",
        ]),
        ut = I.default.div.withConfig({ componentId: "sc-1u8nu73-13" })(
          [
            "display:flex;justify-content:center;margin:48px auto;padding:0 16px;p{margin:0;color:",
            ";line-height:1.33;}&:first-child{margin-top:32px;}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.note;
          }.bind(void 0)
        ),
        dt = I.default.div.withConfig({ componentId: "sc-1u8nu73-14" })([
          "width:600px;",
        ]),
        bt = Object(I.default)(b.a).withConfig({
          componentId: "sc-1u8nu73-15",
        })(
          [
            "text-decoration:none;color:",
            ";font-size:14px;line-height:1.5;&:hover,&:focus{color:",
            ";}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.note;
          }.bind(void 0)
        ),
        ht = d.a.memo(ft);
      function ft(t) {
        var e = t.count,
          n = Object(_.o)(),
          i = Object(r.a)(n, 1)[0],
          a = Object(S.y)();
        return Object(o.jsxs)(jt, {
          children: [
            a(e),
            " ".concat(i("�뉐춻")),
            Object(o.jsx)(_.a, {
              children: i("竊덅き雅녺쎅若�: %(duration)竊�", {
                duration: Object(S.h)(
                  c.default.duration(Math.round(e / 500), "m")
                ),
              }),
            }),
          ],
        });
      }
      var jt = I.default.div.withConfig({ componentId: "sc-1u8nu73-16" })(
          ["color:", ";font-size:12px;line-height:1.33;margin:18px 0;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0)
        ),
        pt = I.default.div.withConfig({ componentId: "sc-1u8nu73-17" })(
          [
            "flex:none;align-self:flex-start;margin:-12px 24px 0 -60px;position:relative;overflow:hidden;width:136px;max-height:192px;border-radius:4px;line-height:0;font-size:0;@media (",
            "){margin-right:12px;margin-left:0;}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.media.desktopMedium;
          }.bind(void 0)
        ),
        Ot = Object(I.default)(h.a).withConfig({
          componentId: "sc-1u8nu73-18",
        })([
          "&::after{content:'';position:absolute;top:0;left:0;right:0;bottom:0;background:rgba(0,0,0,0.02);}",
        ]),
        mt = I.default.img.withConfig({ componentId: "sc-1u8nu73-19" })([
          "width:136px;height:auto;",
        ]);
      function vt(t) {
        var e = this,
          n = t.workType,
          r = t.id,
          c = t.tagsEditable,
          a = t.isUnlisted,
          s = t.onTagEditClick,
          l = t.children,
          u = Object(v.d)(),
          d = Object(j.c)(
            function (t) {
              return Object(i.a)(this, e), Object(C.c)(t, n, r);
            }.bind(this)
          ),
          b = Object(j.c)(
            function (t) {
              return Object(i.a)(this, e), Object(k.c)(t, n, r);
            }.bind(this)
          ),
          h = Object(S.e)(
            Object(j.c)(
              function (t) {
                return Object(i.a)(this, e), Object(y.a)(t, n, r);
              }.bind(this)
            )
          ),
          f = Object(S.e)(
            Object(j.c)(
              function (t) {
                return Object(i.a)(this, e), Object(y.b)(t, n, r);
              }.bind(this)
            )
          ),
          p = Object(S.e)(
            Object(j.c)(
              function (t) {
                return Object(i.a)(this, e), Object(w.a)(t, n, r);
              }.bind(this)
            )
          );
        if (!d) return null;
        var O = Object(S.e)(b).tags.map(
          function (t) {
            return Object(i.a)(this, e), t.tag;
          }.bind(this)
        );
        return Object(o.jsx)(Y, {
          isLoggedIn: u,
          isUnlisted: a,
          workData: d,
          tags: O,
          tagsEditable: c,
          onTagEditClick: s,
          contestBanners: h,
          contestData: f,
          requestData: p,
          children: l,
        });
      }
    },
    1366: function (t, e, n) {
      "use strict";
      n.r(e),
        n.d(e, "illustCss", function () {
          return k;
        });
      n(85), n(65), n(479);
      var i = n(4),
        r = n(1),
        o = n(8),
        c = n(0),
        a = n(9),
        s = (n(28), n(44)),
        l = n(2);
      var u = n(6),
        d = n.n(u),
        b = n(507),
        h = n(51),
        f = n(89),
        j = n(3),
        p = n(297),
        O = n(26),
        m = n(95);
      function v(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function g(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? v(Object(n), !0).forEach(function (e) {
                Object(i.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : v(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      e.default = d.a.forwardRef(function (t, e) {
        var n = this,
          i = t.urls.original,
          s = t.page,
          l = t.shortcutVk,
          u = t.gtmClass,
          d = t.noZoom,
          h = t.onClick,
          f = t.forceOriginalSize,
          j = Object(b.b)({ rootMargin: "300% 0px", triggerOnce: !0 }),
          O = Object(o.a)(j, 2),
          m = O[0],
          v = O[1],
          y = Object(a.j)(1 === s),
          k = Object(o.a)(y, 2),
          C = k[0],
          I = k[1],
          S = f;
        Object(a.d)(
          function () {
            Object(r.a)(this, n), v && I(!0);
          }.bind(this),
          [v]
        );
        var P = Object(a.a)(
            function (t) {
              if ((Object(r.a)(this, n), void 0 !== t)) {
                if (0 !== t.button) return;
                if (h && (h(t, s), t.isDefaultPrevented())) return;
                t.preventDefault();
              }
            }.bind(this),
            [h, s]
          ),
          D = Object(a.a)(
            function (t) {
              Object(r.a)(this, n), h && h(t, s);
            }.bind(this),
            [h, s]
          );
        return Object(c.jsx)(w, {
          role: "presentation",
          first: s <= 1,
          ref: e,
          children: Object(c.jsxs)(_, {
            zoom: !d,
            href: i,
            className: u ? "gtm-".concat(u) : void 0,
            target: "_blank",
            rel: "noopener",
            onClick: P,
            ref: m,
            children: [
              Object(c.jsx)(
                x,
                g(g({}, t), {}, { inView: C, isDisplayOriginalSize: !!S })
              ),
              void 0 !== l &&
                Object(c.jsx)(p.default, { vk: l, onShortcut: D }),
            ],
          }),
        });
      });
      var x = Object(h.connect)(function (t, e) {
        var n = Object(m.c)(t, j.q.Illust, e.illustId),
          i = n && Object(O.e)(t, n.userId);
        return { userName: i && i.name };
      })(function (t) {
        var e,
          n,
          i = t.alt,
          r = t.urls,
          o = r.original,
          u = r.regular,
          d = r.small,
          b = t.width,
          h = t.height,
          j = t.inView,
          p = t.isDisplayOriginalSize,
          O = {
            height: y(
              ((e = "desktopMedium"),
              (n = Object(a.b)(l.ThemeContext).media[e]),
              Object(a.c)("".concat(e, ": ").concat(n)),
              Object(s.o)("(".concat(n, ")"))),
              b,
              h,
              p
            ),
          };
        return j
          ? f.a()
            ? Object(c.jsx)(
                C,
                {
                  alt: i,
                  src: u,
                  width: b,
                  height: h,
                  style: O,
                  isDisplayOriginalSize: p,
                },
                d
              )
            : Object(c.jsx)(
                C,
                {
                  alt: i,
                  src: p ? o : u,
                  srcSet: [
                    "".concat(encodeURI(d), " 540w"),
                    encodeURI(p ? o : u),
                  ].join(","),
                  width: b,
                  height: h,
                  style: O,
                  isDisplayOriginalSize: p,
                },
                d
              )
          : Object(c.jsx)(I, {
              role: "img",
              style: O,
              isDisplayOriginalSize: p,
            });
      });
      function y(t, e, n, i) {
        var r = t ? 704 : 912,
          o = (n / e) * Math.min(r, e),
          c = window.innerHeight;
        return i ? (e > 704 ? (704 * n) / e : n) : Math.min(o, c);
      }
      var w = l.default.div.withConfig({ componentId: "sc-1qpw8k9-0" })(
          ["display:block;margin:0;overflow:hidden;", ""],
          function (t) {
            return (
              Object(r.a)(this, void 0),
              t.first && Object(l.css)(["border-radius:8px 8px 0 0;"])
            );
          }.bind(void 0)
        ),
        k = Object(l.css)(
          ["display:block;width:auto;height:auto;max-width:100%;", ""],
          function (t) {
            var e = this;
            return (
              Object(r.a)(this, void 0),
              !t.isDisplayOriginalSize &&
                Object(l.css)(
                  [
                    "max-height:480px;@media screen and (min-height:552px){max-height:100vh;}@media (",
                    "){max-height:320px;}@media screen and (",
                    ") and (min-height:392px){max-height:100vh;}",
                  ],
                  function (t) {
                    return Object(r.a)(this, e), t.theme.media.desktopMedium;
                  }.bind(this),
                  function (t) {
                    return Object(r.a)(this, e), t.theme.media.desktopMedium;
                  }.bind(this)
                )
            );
          }.bind(void 0)
        ),
        C = l.default.img.withConfig({ componentId: "sc-1qpw8k9-1" })(
          ["", ""],
          k
        ),
        I = l.default.div.withConfig({ componentId: "sc-1qpw8k9-2" })(
          ["", " width:100%;"],
          k
        ),
        _ = l.default.a.withConfig({ componentId: "sc-1qpw8k9-3" })(
          [
            "display:flex;align-items:center;justify-content:center;line-height:0;font-size:0;min-width:200px;min-height:480px;@media screen and (max-height:552px){min-height:calc(100vh - 48px - 24px);}@media (",
            "){min-height:320px;}@media screen and (",
            ") and (max-height:392px){min-height:calc(100vh - 48px - 24px);}cursor:",
            ";",
          ],
          function (t) {
            return Object(r.a)(this, void 0), t.theme.media.desktopMedium;
          }.bind(void 0),
          function (t) {
            return Object(r.a)(this, void 0), t.theme.media.desktopMedium;
          }.bind(void 0),
          function (t) {
            return (
              Object(r.a)(this, void 0),
              t.zoom ? "zoom-in" : void 0 !== t.onClick ? "pointer" : "default"
            );
          }.bind(void 0)
        );
    },
    1370: function (t, e, n) {
      "use strict";
      n(31), n(176);
      var i = n(1),
        r = n(34),
        o = n(37),
        c = n(36),
        a = n(45),
        s = n(30),
        l = n(0),
        u = n(6),
        d = n.n(u),
        b = n(51),
        h = n(14),
        f = n(5),
        j = n(525),
        p = n(95),
        O = n(130),
        m = n(66),
        v = n(547),
        g = n(3),
        x = n(1274),
        y = n(360),
        w = n(361),
        k = n(2);
      function C(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(s.a)(t);
          if (e) {
            var r = Object(s.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(a.a)(this, n);
        };
      }
      var I = (function (t) {
        Object(c.a)(n, t);
        var e = C(n);
        function n() {
          return Object(r.a)(this, n), e.apply(this, arguments);
        }
        return (
          Object(o.a)(n, [
            {
              key: "componentDidMount",
              value: function () {
                void 0 === this.props.responseCount &&
                  this.props.dispatch(
                    p.g(this.props.workType, { id: this.props.id })
                  );
              },
            },
            {
              key: "componentDidUpdate",
              value: function (t) {
                t.id !== this.props.id &&
                  void 0 === this.props.responseCount &&
                  this.props.dispatch(
                    p.g(this.props.workType, { id: this.props.id })
                  );
              },
            },
            {
              key: "render",
              value: function () {
                var t = this,
                  e = this.props,
                  n = e.responseCount,
                  r = e.responses,
                  o = e.id,
                  c = e.workType;
                return void 0 === n || void 0 === r || 0 === n || null === r
                  ? null
                  : Object(l.jsxs)(y.a, {
                      children: [
                        Object(l.jsx)(w.c, {
                          children: Object(l.jsx)(f.f, {
                            ns: f.m.work.ns,
                            children: function (e) {
                              return (
                                Object(i.a)(this, t),
                                e(
                                  f.m.work.info_by_user
                                    .image_response_response_work
                                )
                              );
                            }.bind(this),
                          }),
                        }),
                        Object(l.jsxs)(_, {
                          children: [
                            Object(l.jsx)(S, {
                              children: r.map(
                                function (e) {
                                  return (
                                    Object(i.a)(this, t),
                                    Object(l.jsx)(
                                      P,
                                      {
                                        children:
                                          (e.type,
                                          g.q.Illust,
                                          Object(l.jsx)(O.d, {
                                            size: m.c.Size184,
                                            type: e.type,
                                            thumbnail: e,
                                          })),
                                      },
                                      e.id
                                    )
                                  );
                                }.bind(this)
                              ),
                            }),
                            n > 6 &&
                              Object(l.jsx)(D, {
                                children: Object(l.jsx)(v.b, {
                                  url: h.responseList({}, { id: o, type: c }),
                                  children: Object(l.jsx)(f.f, {
                                    ns: f.m.action.ns,
                                    children: function (e) {
                                      return (
                                        Object(i.a)(this, t),
                                        e(f.m.action.link.see_more_comment)
                                      );
                                    }.bind(this),
                                  }),
                                }),
                              }),
                          ],
                        }),
                      ],
                    });
              },
            },
          ]),
          n
        );
      })(d.a.Component);
      e.a = Object(b.connect)(function (t, e) {
        var n = e.workType,
          i = e.id;
        return { responseCount: j.a(t, n, i), responses: j.b(t, n, i) };
      })(I);
      var _ = k.default.div.withConfig({ componentId: "sc-12d18zi-0" })(
          ["", ""],
          x.b
        ),
        S = k.default.ul.withConfig({ componentId: "sc-12d18zi-1" })([
          "display:flex;overflow-x:auto;overflow-y:hidden;margin:0 0 48px;padding:0;list-style:none;",
        ]),
        P = k.default.li.withConfig({ componentId: "sc-12d18zi-2" })([
          "& + &{margin-left:24px;}",
        ]),
        D = k.default.div.withConfig({ componentId: "sc-12d18zi-3" })([
          "margin:0 auto;width:600px;",
        ]);
    },
    1371: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return s;
      });
      n(117), n(150);
      var i = n(1),
        r = n(0),
        o = (n(6), n(484)),
        c = n(870),
        a = n(2);
      function s(t) {
        var e = t.comicPromotion;
        return Object(r.jsxs)(c.a, {
          children: [
            Object(r.jsx)(l, {
              href: e.workUrl,
              targetBlank: !0,
              rel: "noopener",
              gtmClass: "work-promotion-creatorscomic-thumbnail",
              children: Object(r.jsx)(u, { src: e.imgSrc }),
            }),
            Object(r.jsx)(d, { children: e.magazine }),
            Object(r.jsx)(b, {
              href: e.workUrl,
              targetBlank: !0,
              rel: "noopener",
              gtmClass: "work-promotion-creatorscomic-title",
              children: e.title,
            }),
            Object(r.jsx)(h, { children: e.description }),
          ],
        });
      }
      var l = Object(a.default)(o.a).withConfig({ componentId: "sc-5gzr3-0" })([
          "display:block;margin-bottom:8px;",
        ]),
        u = a.default.img.withConfig({ componentId: "sc-5gzr3-1" })([
          "width:100%;border-radius:4px;",
        ]),
        d = a.default.p.withConfig({ componentId: "sc-5gzr3-2" })(
          ["margin-bottom:4px;color:", ";font-size:12px;line-height:1;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0)
        ),
        b = Object(a.default)(o.a).withConfig({ componentId: "sc-5gzr3-3" })(
          [
            "display:inline-block;margin-bottom:8px;font-size:14px;font-weight:bold;text-decoration:none;color:",
            ";",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.default;
          }.bind(void 0)
        ),
        h = a.default.p.withConfig({ componentId: "sc-5gzr3-4" })(
          ["margin:0;line-height:16px;font-size:12px;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0)
        );
    },
    1373: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return a;
      }),
        n.d(e, "b", function () {
          return s;
        });
      n(28), n(52), n(54);
      var i = n(1),
        r = n(248);
      function o(t, e) {
        var n;
        if ("undefined" == typeof Symbol || null == t[Symbol.iterator]) {
          if (
            Array.isArray(t) ||
            (n = (function (t, e) {
              if (!t) return;
              if ("string" == typeof t) return c(t, e);
              var n = Object.prototype.toString.call(t).slice(8, -1);
              "Object" === n && t.constructor && (n = t.constructor.name);
              if ("Map" === n || "Set" === n) return Array.from(t);
              if (
                "Arguments" === n ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)
              )
                return c(t, e);
            })(t)) ||
            (e && t && "number" == typeof t.length)
          ) {
            n && (t = n);
            var i = 0,
              r = function () {};
            return {
              s: r,
              n: function () {
                return i >= t.length
                  ? { done: !0 }
                  : { done: !1, value: t[i++] };
              },
              e: function (t) {
                throw t;
              },
              f: r,
            };
          }
          throw new TypeError(
            "Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
          );
        }
        var o,
          a = !0,
          s = !1;
        return {
          s: function () {
            n = t[Symbol.iterator]();
          },
          n: function () {
            var t = n.next();
            return (a = t.done), t;
          },
          e: function (t) {
            (s = !0), (o = t);
          },
          f: function () {
            try {
              a || null == n.return || n.return();
            } finally {
              if (s) throw o;
            }
          },
        };
      }
      function c(t, e) {
        (null == e || e > t.length) && (e = t.length);
        for (var n = 0, i = new Array(e); n < e; n++) i[n] = t[n];
        return i;
      }
      function a(t, e) {
        l(t, e, 15552e6, "viewed_illust_ids", "viewed_illust_timestamp");
      }
      function s(t, e) {
        l(t, e, 15552e6, "viewed_novel_ids", "viewed_novel_timestamp");
      }
      function l(t, e, n, c, a) {
        var s = this,
          l = d(r.a("".concat(c, "_").concat(t))) || [],
          b = [e].concat(
            l.filter(
              function (t) {
                return Object(i.a)(this, s), t !== "".concat(e);
              }.bind(this)
            )
          ),
          h = b.slice(0, 50),
          f = d(r.a("".concat(a, "_").concat(t))) || {};
        f["".concat(e)] = +new Date() / 1e3;
        var j,
          p = o(b.slice(50));
        try {
          for (p.s(); !(j = p.n()).done; ) {
            delete f[j.value];
          }
        } catch (O) {
          p.e(O);
        } finally {
          p.f();
        }
        r.c("".concat(c, "_").concat(t), u(h, n)),
          r.c("".concat(a, "_").concat(t), u(f, n));
      }
      function u(t, e) {
        return {
          data: t,
          expires: e instanceof Date ? e : +new Date() + (e || 0),
        };
      }
      function d(t) {
        if (t && "expires" in t) {
          var e = t.data,
            n = t.expires;
          if (void 0 !== n) return +new Date() > n ? void 0 : e;
        }
        return t;
      }
    },
    1378: function (t, e, n) {
      t.exports = {
        wrapper: "SBWE0rt",
        item: "_31R-1qr",
        content: "_-1GcGDy",
        count: "YYBMYr_",
      };
    },
    1379: function (t, e, n) {
      t.exports = { overflow: "_2orEk2K", container: "_3D7iju8" };
    },
    1380: function (t, e, n) {
      t.exports = { clipper: "_1w1BC2t", popupRoot: "Xm8JaLK" };
    },
    1381: function (t, e, n) {
      t.exports = { anchor: "_2bqNGCd", disabled: "_17_aa_8 _2bqNGCd" };
    },
    1382: function (t, e, n) {
      t.exports = {
        container: "_3gaSOts",
        profileIcon: "_2KvfkTc",
        form: "_2I8tQBK",
        input: "_1cfMqxI",
        formNologin: "_1APlg2e _2I8tQBK",
        toolbar: "_261DuyD",
      };
    },
    1383: function (t, e, n) {
      t.exports = { emoji: "_2sgsdWB" };
    },
    1384: function (t, e, n) {
      t.exports = {
        commentContainer: "_1efxis9",
        profileIcon: "_2AkUSrI",
        child: "_1f-UoQ3",
        userName: "_3LIYHOI",
        badge: "_2JMgDaw",
        own: "_2HKMvu4",
        creator: "l6DTSU_",
        commentContentWrapper: "aRr02hb",
        date: "_1p69bvL",
        stampContainer: "sg5GSax",
        stamp: "_36qSqYX",
        commentActionContainer: "_3FlD_n8",
        spa: "_Heo7R7",
        spacer: "_5MYWzsT",
        deleteButton: "_3couS8-",
        replyButton: "ap6h3Lk",
      };
    },
    1385: function (t, e, n) {
      t.exports = {
        list: "_3y7gmhe",
        item: "_28eYrg2",
        showMoreButtonContainer: "MpxBDJJ",
        showMoreButton: "_28zR1MQ",
        busy: "Gng6Hzu",
        loading: "_38wFEJx",
      };
    },
    1386: function (t, e, n) {
      t.exports = { indent: "_3pYCYlY" };
    },
    1387: function (t, e, n) {
      t.exports = {
        wrapper: "_3Q6patW",
        spa: "TMe1nqF",
        rootList: "_3WHHaYz",
        rootItem: "N1ZIOon",
        buttonArea: "_3NKQFrD",
        showMoreButton: "_1Hom0qN",
        busy: "_1THbaXb",
        loadingShowMore: "tjucrD5",
        loadingArea: "N6-Di_z",
        noItem: "_9oLV_Hn",
      };
    },
    1388: function (t, e, n) {
      t.exports = { label: "_3Y1GvH8", spaLabel: "_2gZAorr _3Y1GvH8" };
    },
    1389: function (t, e, n) {
      "use strict";
      var i = n(1),
        r = n(0),
        o = (n(6), n(51)),
        c = n(1070),
        a = n(393),
        s = n(167),
        l = n(399),
        u = n(405),
        d = n(14),
        b = n(64),
        h = n(26),
        f = n(1456),
        j = n(1346),
        p = n(2);
      e.a = Object(o.connect)(function (t, e) {
        return { isSelf: Object(h.k)(t) === e.userId };
      })(function (t) {
        var e = t.id,
          n = t.isSelf,
          i = t.userId,
          o = t.userImage,
          a = t.userName,
          l = t.workType,
          h = Object(b.a)(i);
        return Object(r.jsxs)(v, {
          children: [
            Object(r.jsxs)(g, {
              children: [
                Object(r.jsx)(x, {
                  children: Object(r.jsxs)(m, {
                    userId: i,
                    children: [
                      Object(r.jsx)(s.a, {
                        userId: i,
                        userName: a,
                        url: o,
                        overWhite: !0,
                        card: !1,
                      }),
                      Object(r.jsx)(y, {
                        children: Object(r.jsxs)(w, {
                          userId: i,
                          url: d.user.member({}, { id: i }),
                          card: !1,
                          children: [
                            Object(r.jsx)("div", { children: a }),
                            h && Object(r.jsx)(u.a, {}),
                          ],
                        }),
                      }),
                    ],
                  }),
                }),
                !n &&
                  Object(r.jsx)(c.a, {
                    userId: i,
                    showRecommend: !0,
                    overWhite: !0,
                  }),
                Object(r.jsx)(O, {
                  gtmClass: "work-main-see-more",
                  workType: l,
                  userId: i,
                }),
              ],
            }),
            Object(r.jsx)(f.a, {
              workType: l,
              userId: i,
              currentId: e,
              full: !0,
            }),
          ],
        });
      });
      var O = Object(p.default)(j.a).withConfig({
          componentId: "sc-10gpz4q-0",
        })(["margin-left:auto;"]),
        m = Object(p.default)(a.a).withConfig({ componentId: "sc-10gpz4q-1" })([
          "display:flex;align-items:center;",
        ]),
        v = p.default.div.withConfig({ componentId: "sc-10gpz4q-2" })([
          "padding-top:16px;",
        ]),
        g = p.default.div.withConfig({ componentId: "sc-10gpz4q-3" })([
          "margin:0 auto;padding:0 16px;max-width:600px;display:flex;align-items:center;",
        ]),
        x = p.default.h2.withConfig({ componentId: "sc-10gpz4q-4" })([
          "display:flex;align-items:center;margin:0;font-size:1em;",
        ]),
        y = p.default.div.withConfig({ componentId: "sc-10gpz4q-5" })([
          "display:grid;margin:0 8px;",
        ]),
        w = Object(p.default)(l.a).withConfig({ componentId: "sc-10gpz4q-6" })(
          ["font-weight:bold;color:", ";text-decoration:none;"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text1;
          }.bind(void 0)
        );
    },
    1454: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return ge;
      });
      var i = n(1),
        r = n(8),
        o = n(0),
        c = n(9),
        a = n(6),
        s = n.n(a),
        l = n(102),
        u = n.n(l),
        d = n(5),
        b = (n(28), n(29), n(70), n(7)),
        h = n.n(b),
        f = (n(39), n(12)),
        j = n(34),
        p = n(37),
        O = n(24),
        m = n(36),
        v = n(45),
        g = n(30),
        x = n(4),
        y = n(51),
        w = n(68),
        k = n(160),
        C = n(724),
        I = n(1059),
        _ = (n(47), n(58), n(60), n(172), n(596)),
        S = Object(_.a)("images/emoji/").href,
        P = [
          D(101, "normal"),
          D(102, "surprise"),
          D(103, "serious"),
          D(104, "heaven"),
          D(105, "happy"),
          D(106, "excited"),
          D(107, "sing"),
          D(108, "cry"),
          D(201, "normal2"),
          D(202, "shame2"),
          D(203, "love2"),
          D(204, "interesting2"),
          D(205, "blush2"),
          D(206, "fire2"),
          D(207, "angry2"),
          D(208, "shine2"),
          D(209, "panic2"),
          D(301, "normal3"),
          D(302, "satisfaction3"),
          D(303, "surprise3"),
          D(304, "smile3"),
          D(305, "shock3"),
          D(306, "gaze3"),
          D(307, "wink3"),
          D(308, "happy3"),
          D(309, "excited3"),
          D(310, "love3"),
          D(401, "normal4"),
          D(402, "surprise4"),
          D(403, "serious4"),
          D(404, "love4"),
          D(405, "shine4"),
          D(406, "sweat4"),
          D(407, "shame4"),
          D(408, "sleep4"),
          D(501, "heart"),
          D(502, "teardrop"),
          D(503, "star"),
        ];
      function D(t, e) {
        var n =
          arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : e;
        return {
          name: n,
          keywords: [e],
          short_names: ["(".concat(e, ")")],
          imageUrl: new URL("".concat(t, ".png"), S).href,
        };
      }
      n(65), n(519);
      var T = n(1064),
        R = n(1499),
        z = (n(1462), n(31), n(11)),
        N = n(1378);
      function M(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var q = (function (t) {
          Object(m.a)(n, t);
          var e = M(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(
                Object(O.a)(t),
                "handleClick",
                function (e) {
                  Object(i.a)(this, r);
                  var n = t.props;
                  (0, n.onClick)(n.stamp, e);
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "render",
                value: function () {
                  return Object(o.jsx)("div", {
                    role: "listitem",
                    className: N.wrapper,
                    children: Object(o.jsx)("button", {
                      type: "button",
                      disabled: this.props.disabled,
                      className: N.item,
                      style: {
                        backgroundImage: Object(z.d)(this.props.stamp.imageUrl),
                      },
                      "aria-label": "Stamp ".concat(this.props.stamp.id),
                      onClick: this.handleClick,
                      children:
                        this.props.count &&
                        Object(o.jsx)("div", {
                          role: "presentation",
                          className: N.content,
                          children: Object(o.jsxs)("svg", {
                            className: N.count,
                            viewBox: "0 0 1 16",
                            children: [
                              Object(o.jsx)("text", {
                                x: 1,
                                y: 16,
                                textAnchor: "end",
                                alignmentBaseline: "after-edge",
                                fill: "none",
                                children: this.props.count,
                              }),
                              Object(o.jsx)("text", {
                                x: 1,
                                y: 16,
                                textAnchor: "end",
                                alignmentBaseline: "after-edge",
                                stroke: "none",
                                children: this.props.count,
                              }),
                            ],
                          }),
                        }),
                    }),
                  });
                },
              },
            ]),
            n
          );
        })(s.a.PureComponent),
        U = n(1379);
      n(111);
      function E(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function L(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? E(Object(n), !0).forEach(function (e) {
                Object(x.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : E(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      var A = { hidden: !1 },
        F = Object(_.a)("images/stamp/generated-stamps/").href,
        B = [].concat(
          H(3),
          H(4),
          H(2),
          H(1),
          H(6, { hidden: !0 }),
          H(7, { hidden: !0 })
        );
      function W(t, e) {
        return L(
          L(
            {
              id: "".concat(t),
              imageUrl: new URL("".concat(t, "_s.jpg?20180605"), F).href,
            },
            A
          ),
          e
        );
      }
      function H(t, e) {
        var n = this,
          r =
            arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : 10;
        return Array.from(
          { length: r },
          function (r, o) {
            return Object(i.a)(this, n), W(100 * t + o + 1, e);
          }.bind(this)
        );
      }
      function V(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var G = (function (t) {
          Object(m.a)(n, t);
          var e = V(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(
                Object(O.a)(t),
                "handleClick",
                function (e) {
                  Object(i.a)(this, r), (0, t.props.onClick)(e);
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this;
                  return Object(o.jsx)("div", {
                    className: U.overflow,
                    children: Object(o.jsx)("div", {
                      role: "group",
                      className: U.container,
                      children: B.map(
                        function (e) {
                          return (
                            Object(i.a)(this, t),
                            e.hidden
                              ? null
                              : Object(o.jsx)(
                                  q,
                                  { stamp: e, onClick: this.handleClick },
                                  e.id
                                )
                          );
                        }.bind(this)
                      ),
                    }),
                  });
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        Z =
          (n(96),
          n(158),
          n(128),
          n(131),
          n(132),
          n(133),
          n(134),
          n(135),
          n(136),
          n(137),
          n(138),
          n(139),
          n(140),
          n(141),
          n(142),
          n(143),
          n(144),
          n(145),
          n(146),
          n(55)),
        Y = n.n(Z),
        X = n(1324);
      function $(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var K = (function (t) {
          Object(m.a)(n, t);
          var e = $(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(
                Object(O.a)(t),
                "handleClick",
                function (e) {
                  Object(i.a)(this, r);
                  var n = t.props;
                  (0, n.onClick)(n.value, e);
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "render",
                value: function () {
                  return Object(o.jsx)("button", {
                    type: "button",
                    className: Y()(X.tab, this.props.current && X.current),
                    disabled: this.props.disabled,
                    onClick: this.handleClick,
                    children: this.props.children,
                  });
                },
              },
            ]),
            n
          );
        })(s.a.PureComponent),
        Q = n(193),
        J = n.n(Q),
        tt = n(287);
      function et(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var nt = (function (t) {
          Object(m.a)(n, t);
          var e = et(n);
          function n(t) {
            var r,
              o = this;
            Object(j.a)(this, n),
              (r = e.call(this, t)),
              Object(x.a)(Object(O.a)(r), "state", {
                currentTabValue:
                  r.props.defaultTabValue ||
                  (r.props.tabs.length > 0 ? r.props.tabs[0].value : void 0),
              }),
              Object(x.a)(
                Object(O.a)(r),
                "handleClick",
                function (t) {
                  var e = this;
                  Object(i.a)(this, o);
                  var n = r.props.tabs.find(
                    function (n) {
                      return Object(i.a)(this, e), n.value === t;
                    }.bind(this)
                  );
                  r.setState({ currentTabValue: n && n.value });
                }.bind(this)
              );
            var c = new Set(
              r.props.tabs.map(
                function (t) {
                  return Object(i.a)(this, o), t.value;
                }.bind(this)
              )
            );
            return (
              J()(
                c.size === r.props.tabs.length,
                'CenteredTabBar: All tab "value"s must be unique'
              ),
              r
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "componentDidUpdate",
                value: function (t) {
                  var e = this;
                  if (!Object(tt.shallowEqual)(this.props.tabs, t.tabs)) {
                    var n = new Set(
                      this.props.tabs.map(
                        function (t) {
                          return Object(i.a)(this, e), t.value;
                        }.bind(this)
                      )
                    );
                    J()(
                      n.size === t.tabs.length,
                      'CenteredTabBar: All tab "value"s must be unique'
                    );
                    var r = this.state.currentTabValue;
                    this.props.tabs.some(
                      function (t) {
                        return Object(i.a)(this, e), t.value === r;
                      }.bind(this)
                    ) ||
                      this.setState({
                        currentTabValue:
                          this.props.tabs.length > 0
                            ? this.props.tabs[0].value
                            : void 0,
                      });
                  }
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this,
                    e = this.props.children;
                  return Object(o.jsxs)("div", {
                    className: Y()(
                      X.wrapper,
                      this.props.tabsOnBottom && X.reverse
                    ),
                    children: [
                      Object(o.jsx)("nav", {
                        className: X.nav,
                        children: this.props.tabs.map(
                          function (e) {
                            return (
                              Object(i.a)(this, t),
                              Object(o.jsx)(
                                K,
                                {
                                  current:
                                    e.value === this.state.currentTabValue,
                                  value: e.value,
                                  onClick: this.handleClick,
                                  children: e.label,
                                },
                                e.value
                              )
                            );
                          }.bind(this)
                        ),
                      }),
                      "function" == typeof e
                        ? void 0 !== this.state.currentTabValue &&
                          e(
                            this.props.tabs.find(
                              function (e) {
                                return (
                                  Object(i.a)(this, t),
                                  e.value === this.state.currentTabValue
                                );
                              }.bind(this)
                            )
                          )
                        : e,
                    ],
                  });
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        it = n(27);
      function rt(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function ot(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? rt(Object(n), !0).forEach(function (e) {
                Object(x.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : rt(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function ct(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var at = (function (t) {
          Object(m.a)(n, t);
          var e = ct(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(Object(O.a)(t), "ref", void 0),
              Object(x.a)(
                Object(O.a)(t),
                "saveRef",
                function (e) {
                  Object(i.a)(this, r), (t.ref = e);
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleMouseDown",
                function (e) {
                  Object(i.a)(this, r);
                  for (
                    var n = e.target;
                    null !== n && n !== e.currentTarget;
                    n = n.parentNode
                  )
                    if (n === t.ref) return;
                  (0, t.props.onClickOutside)(e);
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "componentDidMount",
                value: function () {
                  document.addEventListener(
                    "mousedown",
                    this.handleMouseDown,
                    !1
                  );
                },
              },
              {
                key: "componentWillUnmount",
                value: function () {
                  document.removeEventListener(
                    "mousedown",
                    this.handleMouseDown
                  );
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this.props,
                    e = (t.onClickOutside, Object(it.a)(t, ["onClickOutside"]));
                  return Object(o.jsx)(
                    "div",
                    ot(ot({}, e), {}, { ref: this.saveRef })
                  );
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        st = n(373),
        lt = n(1380);
      function ut(t) {
        var e = this,
          n = Object(c.i)(!1),
          a = Object(c.j)(0),
          s = Object(r.a)(a, 2),
          l = s[0],
          u = s[1],
          b = Object(c.j)(0),
          h = Object(r.a)(b, 2),
          f = h[0],
          j = h[1],
          p = Object(c.a)(
            function (t) {
              Object(i.a)(this, e);
              var n = window,
                r = n.scrollY,
                o = n.scrollX,
                c = t.getBoundingClientRect();
              return { bottom: r + c.top - 10, left: o + c.left + c.width / 2 };
            }.bind(this),
            []
          ),
          O = Object(c.a)(
            function (n) {
              Object(i.a)(this, e),
                n.custom
                  ? t.onClick("(".concat(n.name, ")"))
                  : t.onClick(n.native);
            }.bind(this),
            [t]
          ),
          m = Object(c.a)(
            function (n) {
              Object(i.a)(this, e), t.onStampClick(n);
            }.bind(this),
            [t]
          ),
          v = Object(c.a)(
            function (n) {
              Object(i.a)(this, e);
              var r = n.value;
              return "emoji" === r
                ? Object(o.jsx)(R.a, {
                    set: "twitter",
                    autoFocus: !0,
                    emoji: t.emoji,
                    native: !0,
                    title: "",
                    include: ["custom"],
                    custom: P,
                    skin: 1,
                    perLine: 10,
                    onClick: O,
                  })
                : "stamp" === r
                ? Object(o.jsx)("div", {
                    className: lt.clipper,
                    children: Object(o.jsx)(G, { onClick: m }),
                  })
                : null;
            }.bind(this),
            [O, m, t.emoji]
          );
        return (
          Object(c.d)(
            function () {
              var r = this;
              Object(i.a)(this, e);
              var o = Object(T.a)(
                function () {
                  if ((Object(i.a)(this, r), n)) {
                    var e = p(t.anchor);
                    u(e.left), j(e.bottom);
                  }
                }.bind(this),
                1e3 / 60,
                { maxWait: 1e3 / 15 }
              );
              return (
                (n.current = !0),
                window.addEventListener("resize", o, !0),
                function () {
                  Object(i.a)(this, r),
                    window.removeEventListener("resize", o),
                    (n.current = !1);
                }.bind(this)
              );
            }.bind(this),
            [p, t.anchor]
          ),
          Object(c.d)(
            function () {
              Object(i.a)(this, e);
              var n = p(t.anchor);
              u(n.left), j(n.bottom);
            }.bind(this),
            [p, t.anchor]
          ),
          Object(o.jsx)(st.a, {
            children: Object(o.jsx)(at, {
              onClickOutside: t.onClickOutside,
              className: lt.popupRoot,
              style: {
                position: "absolute",
                transform: "translate(-50%, -100%)",
                left: l,
                top: f,
              },
              children: t.stampOnly
                ? Object(o.jsx)("div", {
                    className: lt.clipper,
                    children: Object(o.jsx)(G, { onClick: m }),
                  })
                : Object(o.jsx)(d.d, {
                    children: function (t) {
                      return (
                        Object(i.a)(this, e),
                        Object(o.jsx)(nt, {
                          tabs: [
                            { label: t("永득뻼耶�"), value: "emoji" },
                            { label: t("�밤궭�녈깤"), value: "stamp" },
                          ],
                          children: v,
                        })
                      );
                    }.bind(this),
                  }),
            }),
          })
        );
      }
      var dt = n(1381);
      function bt(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function ht(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? bt(Object(n), !0).forEach(function (e) {
                Object(x.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : bt(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function ft(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var jt = (function (t) {
          Object(m.a)(n, t);
          var e = ft(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(Object(O.a)(t), "state", { open: !1 }),
              Object(x.a)(Object(O.a)(t), "ref", null),
              Object(x.a)(Object(O.a)(t), "relatedTarget", null),
              Object(x.a)(
                Object(O.a)(t),
                "saveRef",
                function (e) {
                  Object(i.a)(this, r), (t.ref = e);
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleClick",
                function (e) {
                  Object(i.a)(this, r),
                    e.target !== e.currentTarget ||
                      t.props.disabled ||
                      (e.preventDefault(),
                      t.state.open
                        ? t.setState({ open: !1 })
                        : t.setState({
                            customPreviewEmoji: ht(
                              ht(
                                {},
                                P[
                                  Math.floor(Math.random() * P.length) %
                                    P.length
                                ]
                              ),
                              {},
                              { custom: !0, length: 1 }
                            ),
                            open: !0,
                          }));
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleFocus",
                function (e) {
                  Object(i.a)(this, r),
                    t.state.open || (t.relatedTarget = e.relatedTarget),
                    e.currentTarget.blur();
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handlePick",
                function (e) {
                  Object(i.a)(this, r),
                    (0, t.props.onClick)(e),
                    t.setState({ open: !1 });
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleStampPick",
                function (e) {
                  Object(i.a)(this, r),
                    (0, t.props.onStampClick)(e),
                    t.setState({ open: !1 });
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleClickOutside",
                function (e) {
                  Object(i.a)(this, r),
                    e.target !== t.ref && t.setState({ open: !1 });
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              { key: "componentDidMount", value: function () {} },
              {
                key: "componentDidUpdate",
                value: function (t, e) {
                  e.open &&
                    !this.state.open &&
                    null !== this.relatedTarget &&
                    (this.relatedTarget.focus(), (this.relatedTarget = null));
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this.props.disabled,
                    e = this.state,
                    n = e.open,
                    i = e.customPreviewEmoji;
                  return Object(o.jsx)("div", {
                    tabIndex: t ? void 0 : -1,
                    className: t ? dt.disabled : dt.anchor,
                    onClick: this.handleClick,
                    onFocus: this.handleFocus,
                    ref: this.saveRef,
                    children:
                      !t &&
                      n &&
                      Object(o.jsx)(ut, {
                        anchor: this.ref,
                        emoji: i,
                        onClick: this.handlePick,
                        onClickOutside: this.handleClickOutside,
                        onStampClick: this.handleStampPick,
                      }),
                  });
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        pt = n(1382),
        Ot = n(2),
        mt = n(14);
      function vt(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var gt = Object(w.a)(
          function () {
            var t = this;
            return (
              Object(i.a)(this, void 0),
              Promise.resolve()
                .then(function () {
                  if (!n.m[297]) {
                    var t = new Error(
                      "Module '297' is not available (weak dependency)"
                    );
                    throw ((t.code = "MODULE_NOT_FOUND"), t);
                  }
                  return n(297);
                })
                .catch(
                  function () {
                    var e = this;
                    return (
                      Object(i.a)(this, t),
                      {
                        default: function () {
                          return Object(i.a)(this, e), null;
                        }.bind(this),
                      }
                    );
                  }.bind(this)
                )
            );
          }.bind(void 0)
        ),
        xt = Object(w.a)(
          function () {
            var t = this;
            return (
              Object(i.a)(this, void 0),
              Promise.resolve()
                .then(function () {
                  if (!n.m[1058]) {
                    var t = new Error(
                      "Module '1058' is not available (weak dependency)"
                    );
                    throw ((t.code = "MODULE_NOT_FOUND"), t);
                  }
                  return n(1058);
                })
                .catch(
                  function () {
                    var e = this;
                    return (
                      Object(i.a)(this, t),
                      {
                        default: function () {
                          return Object(i.a)(this, e), null;
                        }.bind(this),
                      }
                    );
                  }.bind(this)
                )
            );
          }.bind(void 0)
        );
      var yt,
        wt = (function (t) {
          Object(m.a)(n, t);
          var e = vt(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(Object(O.a)(t), "state", { busy: !1 }),
              Object(x.a)(Object(O.a)(t), "mounted", !1),
              Object(x.a)(Object(O.a)(t), "textInputRef", void 0),
              Object(x.a)(
                Object(O.a)(t),
                "handleSubmit",
                (function () {
                  var e = Object(f.a)(
                    h.a.mark(function e(n) {
                      var i, r, o;
                      return h.a.wrap(
                        function (e) {
                          for (;;)
                            switch ((e.prev = e.next)) {
                              case 0:
                                return (
                                  n.preventDefault(),
                                  (e.prev = 1),
                                  t.setState({ busy: !0 }),
                                  (i = t.props),
                                  (r = i.onSubmit),
                                  (o = i.value),
                                  (e.next = 6),
                                  Promise.resolve(r(o))
                                );
                              case 6:
                                return (
                                  (e.prev = 6),
                                  t.mounted && t.setState({ busy: !1 }),
                                  e.finish(6)
                                );
                              case 9:
                              case "end":
                                return e.stop();
                            }
                        },
                        e,
                        null,
                        [[1, , 6, 9]]
                      );
                    })
                  );
                  return function (t) {
                    return e.apply(this, arguments);
                  };
                })()
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleNologinFormClick",
                function (t) {
                  Object(i.a)(this, r), t.preventDefault();
                  var e = location.href;
                  location.href = mt.login({}, { return_to: e });
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleEmojiPicked",
                function (e) {
                  Object(i.a)(this, r),
                    t.textInputRef && t.textInputRef.insertText(e);
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleStampClick",
                function (e) {
                  Object(i.a)(this, r), (0, t.props.onSubmitStamp)(e);
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleShortcut",
                function (e) {
                  Object(i.a)(this, r),
                    e.preventDefault(),
                    t.textInputRef.focus();
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "saveTextInputRef",
                function (e) {
                  Object(i.a)(this, r), (t.textInputRef = e);
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "componentDidMount",
                value: function () {
                  this.mounted = !0;
                },
              },
              {
                key: "componentWillUnmount",
                value: function () {
                  this.mounted = !1;
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this,
                    e = this.props,
                    n = e.value,
                    r = e.profileImage,
                    c = e.placeholder,
                    a = e.verified,
                    l = e.onChange,
                    u = e.autoFocus,
                    b = e.isLoggedIn,
                    h = e.shortcutVk,
                    f = this.state.busy,
                    j = Object(o.jsxs)("div", {
                      className: pt.input,
                      children: [
                        Object(o.jsx)(d.f, {
                          ns: d.m.action.ns,
                          children: function (e) {
                            return (
                              Object(i.a)(this, t),
                              Object(o.jsx)(Ct, {
                                onChange: l,
                                value: n,
                                placeholder:
                                  c ||
                                  e(d.m.action.comment.comment_placeholder),
                                multiline: !0,
                                multilineEnterBehavior: I.a.PreferNewline,
                                autoHeight: !0,
                                autoFocus: u,
                                disabled: f || !1 === a || !1 === b,
                                maxLength: 140,
                                ref: this.saveTextInputRef,
                              })
                            );
                          }.bind(this),
                        }),
                        Object(o.jsx)("nav", {
                          className: pt.toolbar,
                          children: Object(o.jsx)(jt, {
                            disabled: !1 === a,
                            onClick: this.handleEmojiPicked,
                            onStampClick: this.handleStampClick,
                          }),
                        }),
                      ],
                    });
                  return Object(o.jsxs)("div", {
                    className: pt.container,
                    children: [
                      Object(o.jsx)("div", {
                        className: pt.profileIcon,
                        children: Object(o.jsx)(C.a, { url: r, size: 32 }),
                      }),
                      Object(o.jsx)(d.f, {
                        ns: d.m.action.ns,
                        children: function (e) {
                          var n = this;
                          return (
                            Object(i.a)(this, t),
                            !1 === b
                              ? Object(o.jsxs)("form", {
                                  onClick: this.handleNologinFormClick,
                                  className: pt.formNologin,
                                  children: [
                                    j,
                                    Object(o.jsx)(k.a, {
                                      primary: !0,
                                      disabled: !0,
                                      children: e(d.m.action.comment.send),
                                    }),
                                  ],
                                })
                              : void 0 === a
                              ? Object(o.jsxs)("form", {
                                  onSubmit: this.handleSubmit,
                                  className: pt.form,
                                  children: [
                                    j,
                                    Object(o.jsx)(k.a, {
                                      primary: !0,
                                      disabled: f,
                                      children: e(d.m.action.comment.send),
                                    }),
                                  ],
                                })
                              : Object(o.jsx)(s.a.Suspense, {
                                  fallback: null,
                                  children: Object(o.jsx)(xt, {
                                    children: function (t) {
                                      return (
                                        Object(i.a)(this, n),
                                        Object(o.jsxs)("form", {
                                          onClick: t,
                                          onSubmit: this.handleSubmit,
                                          className: pt.form,
                                          children: [
                                            j,
                                            Object(o.jsx)(k.a, {
                                              primary: !0,
                                              disabled: f || !a,
                                              forceBubblingWhenDisabled: !a,
                                              noPreventDefault: !a,
                                              children: e(
                                                d.m.action.comment.send
                                              ),
                                            }),
                                          ],
                                        })
                                      );
                                    }.bind(this),
                                  }),
                                })
                          );
                        }.bind(this),
                      }),
                      Object(o.jsx)(s.a.Suspense, {
                        fallback: null,
                        children:
                          void 0 !== h &&
                          Object(o.jsx)(gt, {
                            vk: h,
                            onShortcut: this.handleShortcut,
                          }),
                      }),
                    ],
                  });
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        kt = Object(y.connect)(function (t) {
          return {
            verified:
              t.spa && void 0 !== t.userData
                ? null !== t.userData.self && !t.userData.self.notVerified
                : void 0,
            isLoggedIn:
              t.spa && void 0 !== t.userData
                ? null !== t.userData.self
                : void 0,
          };
        })(wt),
        Ct = Object(Ot.default)(I.b).withConfig({ componentId: "ztmbzk-0" })([
          "&&&{padding-right:32px;}",
        ]),
        It =
          (n(52),
          n(189),
          n(53),
          n(103),
          n(164),
          n(176),
          n(200),
          n(201),
          n(202),
          n(203),
          n(204),
          n(205),
          n(206),
          n(207),
          n(208),
          n(209),
          n(210),
          n(211),
          n(212),
          n(175)),
        _t = n(46),
        St = n.n(_t),
        Pt = n(41),
        Dt = n(522),
        Tt = n(460),
        Rt = n(40),
        zt = n(1296),
        Nt = (n(362), n(276)),
        Mt = n(32),
        qt = (n(54), n(126)),
        Ut = n(173),
        Et = n(1304),
        Lt = n(1383),
        At = n.n(Lt),
        Ft = s.a.memo(Bt);
      function Bt(t) {
        var e = this,
          n = t.text,
          r = t.size,
          c = P.find(
            function (t) {
              return Object(i.a)(this, e), t.name === n;
            }.bind(this)
          );
        return void 0 === c
          ? n
          : Object(o.jsx)("img", {
              src: c.imageUrl,
              width: r,
              height: r,
              className: At.a.emoji,
            });
      }
      function Wt(t, e) {
        Wt = function (t, e) {
          return new o(t, void 0, e);
        };
        var n = Object(qt.a)(RegExp),
          i = RegExp.prototype,
          r = new WeakMap();
        function o(t, e, i) {
          var o = n.call(this, t, e);
          return r.set(o, i || r.get(t)), o;
        }
        function c(t, e) {
          var n = r.get(e);
          return Object.keys(n).reduce(function (e, i) {
            return (e[i] = t[n[i]]), e;
          }, Object.create(null));
        }
        return (
          Object(m.a)(o, n),
          (o.prototype.exec = function (t) {
            var e = i.exec.call(this, t);
            return e && (e.groups = c(e, this)), e;
          }),
          (o.prototype[Symbol.replace] = function (t, e) {
            if ("string" == typeof e) {
              var n = r.get(this);
              return i[Symbol.replace].call(
                this,
                t,
                e.replace(/\$<([^>]+)>/g, function (t, e) {
                  return "$" + n[e];
                })
              );
            }
            if ("function" == typeof e) {
              var o = this;
              return i[Symbol.replace].call(this, t, function () {
                var t = [];
                return (
                  t.push.apply(t, arguments),
                  "object" !== Object(Mt.a)(t[t.length - 1]) && t.push(c(t, o)),
                  e.apply(this, t)
                );
              });
            }
            return i[Symbol.replace].call(this, t, e);
          }),
          Wt.apply(this, arguments)
        );
      }
      function Ht(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      !(function (t) {
        (t[(t.Text = 0)] = "Text"), (t[(t.Emoji = 1)] = "Emoji");
      })(yt || (yt = {}));
      var Vt = (function (t) {
          Object(m.a)(n, t);
          var e = Ht(n);
          function n() {
            return Object(j.a)(this, n), e.apply(this, arguments);
          }
          return (
            Object(p.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this,
                    e = this.props,
                    n = e.children,
                    r = e.emojiSize,
                    c = this.parse(n);
                  return Object(o.jsx)(Et.a, {
                    children: c.map(
                      function (e, n) {
                        return (
                          Object(i.a)(this, t),
                          e.type === yt.Emoji
                            ? Object(o.jsx)(Ft, { text: e.text, size: r }, n)
                            : e.text
                        );
                      }.bind(this)
                    ),
                  });
                },
              },
              {
                key: "parse",
                value: function (t) {
                  for (
                    var e = [],
                      n = Wt(
                        /\(([0-9a-z\u017F\u212A]+)\)|:([0-9a-z\u017F\u212A]+):/gi,
                        { parenEmoji: 1, emoji: 2 }
                      ),
                      i = 0,
                      r = n.exec(t);
                    null !== r;
                    r = n.exec(t)
                  ) {
                    i < r.index &&
                      e.push({ text: t.slice(i, r.index), type: yt.Text });
                    var o = Object(Ut.a)(r),
                      c = o.emoji,
                      a = o.parenEmoji,
                      s = void 0 === a ? c : a;
                    e.push({ text: s, type: yt.Emoji }),
                      (i = r.index + r[0].length);
                  }
                  return (
                    i < t.length &&
                      e.push({ text: t.slice(i, t.length), type: yt.Text }),
                    e
                  );
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        Gt = n(38),
        Zt = n(1384);
      function Yt(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var Xt = Object(w.a)(
          function () {
            var t = this;
            return (
              Object(i.a)(this, void 0),
              Promise.resolve()
                .then(function () {
                  if (!n.m[1058]) {
                    var t = new Error(
                      "Module '1058' is not available (weak dependency)"
                    );
                    throw ((t.code = "MODULE_NOT_FOUND"), t);
                  }
                  return n(1058);
                })
                .catch(
                  function () {
                    var e = this;
                    return (
                      Object(i.a)(this, t),
                      {
                        default: function () {
                          return Object(i.a)(this, e), null;
                        }.bind(this),
                      }
                    );
                  }.bind(this)
                )
            );
          }.bind(void 0)
        ),
        $t = (function (t) {
          Object(m.a)(n, t);
          var e = Yt(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(
                Object(O.a)(t),
                "handleReplyClick",
                function () {
                  Object(i.a)(this, r);
                  var e = t.props;
                  (0, e.onClickReply)(e.comment.id);
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this,
                    e = this.props,
                    n = e.creatorUserId,
                    r = e.viewerUserId,
                    c = e.comment,
                    a = e.child,
                    l = e.disabledForm,
                    u = e.onDeleteComment,
                    b = e.spa,
                    h = e.isLoggedIn;
                  return Object(o.jsx)("div", {
                    className: Zt.commentContainer,
                    children: Object(o.jsx)(d.d, {
                      children: function (e) {
                        var d = this;
                        return (
                          Object(i.a)(this, t),
                          Object(o.jsxs)(o.Fragment, {
                            children: [
                              Object(o.jsx)(Gt.a, {
                                spa: !0,
                                to: mt.user.member({}, { id: c.userId }),
                                className: Y()(
                                  Zt.profileIcon,
                                  "ui-profile-popup",
                                  Object(x.a)({}, Zt.child, a)
                                ),
                                "data-user_id": c.userId,
                                "data-user_name": c.userName,
                                "data-src": c.img,
                                children: Object(o.jsx)(C.a, {
                                  url: c.img,
                                  size: a ? 32 : 40,
                                }),
                              }),
                              Object(o.jsxs)("div", {
                                className: Zt.commentContentWrapper,
                                children: [
                                  Object(o.jsxs)("div", {
                                    className: Zt.userName,
                                    children: [
                                      c.userName,
                                      c.userId === r
                                        ? Object(o.jsx)("span", {
                                            className: Y()(Zt.badge, Zt.own),
                                            children: e("�귙겒��"),
                                          })
                                        : c.userId === n &&
                                          Object(o.jsx)("span", {
                                            className: Y()(
                                              Zt.badge,
                                              Zt.creator
                                            ),
                                            children: e("鵝쒑��"),
                                          }),
                                    ],
                                  }),
                                  Object(o.jsx)(Kt, {
                                    children: c.stampId
                                      ? Object(o.jsx)("div", {
                                          className: Zt.stampContainer,
                                          children: Object(o.jsx)("div", {
                                            className: Zt.stamp,
                                            style: {
                                              backgroundImage: Object(z.d)(
                                                Object(_.a)(
                                                  "images/stamp/generated-stamps/".concat(
                                                    encodeURIComponent(
                                                      "".concat(c.stampId)
                                                    ),
                                                    "_s.jpg?20180605"
                                                  )
                                                ).href
                                              ),
                                            },
                                          }),
                                        })
                                      : Object(o.jsx)(Vt, {
                                          emojiSize: 24,
                                          children: c.comment,
                                        }),
                                  }),
                                  Object(o.jsxs)("div", {
                                    className: Y()(
                                      Zt.commentActionContainer,
                                      b && Zt.spa
                                    ),
                                    children: [
                                      Object(o.jsx)("span", {
                                        className: Zt.date,
                                        children: Object(Nt.c)(
                                          Object(Rt.default)(c.commentDate)
                                        ),
                                      }),
                                      !l &&
                                        (this.props.spa
                                          ? Object(o.jsx)(s.a.Suspense, {
                                              fallback: null,
                                              children: Object(o.jsx)(Xt, {
                                                children: function (t) {
                                                  return (
                                                    Object(i.a)(this, d),
                                                    Object(o.jsx)(Jt, {
                                                      onClick: this
                                                        .handleReplyClick,
                                                      verify: t,
                                                      disableReply: !1 === h,
                                                    })
                                                  );
                                                }.bind(this),
                                              }),
                                            })
                                          : Object(o.jsx)(Jt, {
                                              onClick: this.handleReplyClick,
                                            })),
                                      c.editable &&
                                        Object(o.jsxs)(o.Fragment, {
                                          children: [
                                            Object(o.jsx)("span", {
                                              className: Zt.spacer,
                                              children: "쨌",
                                            }),
                                            Object(o.jsx)("span", {
                                              className: Zt.deleteButton,
                                              onClick: u,
                                              "data-comment-id": c.id,
                                              children: e("�딃솮"),
                                            }),
                                          ],
                                        }),
                                    ],
                                  }),
                                ],
                              }),
                            ],
                          })
                        );
                      }.bind(this),
                    }),
                  });
                },
              },
            ]),
            n
          );
        })(s.a.Component);
      Object(x.a)($t, "defaultProps", { child: !1 });
      var Kt = Ot.default.div.withConfig({ componentId: "sc-15qj8u5-0" })(
          [
            "white-space:pre-wrap;margin:2px 0 6px;line-height:18px;color:",
            ";",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        Qt = Object(y.connect)(function (t) {
          return {
            spa: t.spa,
            isLoggedIn:
              t.spa && void 0 !== t.userData
                ? null !== t.userData.self
                : void 0,
          };
        })($t),
        Jt = (function (t) {
          Object(m.a)(n, t);
          var e = Yt(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(
                Object(O.a)(t),
                "handleClick",
                function (e) {
                  Object(i.a)(this, r);
                  var n = t.props,
                    o = n.onClick,
                    c = n.verify;
                  void 0 !== c && c(e), e.defaultPrevented || o();
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this;
                  return Object(o.jsxs)(o.Fragment, {
                    children: [
                      Object(o.jsx)("span", {
                        className: Zt.spacer,
                        children: "쨌",
                      }),
                      !this.props.disableReply &&
                        Object(o.jsx)(d.d, {
                          children: function (e) {
                            return (
                              Object(i.a)(this, t),
                              Object(o.jsx)("span", {
                                className: Zt.replyButton,
                                onClick: this.handleClick,
                                children: e("瓦붶에"),
                              })
                            );
                          }.bind(this),
                        }),
                    ],
                  });
                },
              },
            ]),
            n
          );
        })(s.a.PureComponent),
        te = n(18);
      function ee(t, e, n) {
        return t.post(
          "/rpc_delete_comment.php",
          {},
          St.a.stringify({ i_id: e, del_id: n })
        );
      }
      function ne(t, e, n) {
        return t.post(
          "/novel/rpc_delete_comment.php",
          {},
          St.a.stringify({ i_id: e, del_id: n })
        );
      }
      var ie = n(1385);
      function re(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var oe = (function (t) {
        Object(m.a)(n, t);
        var e = re(n);
        function n() {
          var t,
            r = this;
          Object(j.a)(this, n);
          for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
            c[a] = arguments[a];
          return (
            (t = e.call.apply(e, [this].concat(c))),
            Object(x.a)(Object(O.a)(t), "state", {
              childComments: null,
              page: 1,
              permaLinkReplyMode: !1,
              busy: !1,
            }),
            Object(x.a)(
              Object(O.a)(t),
              "handleClickShowReplies",
              function () {
                if ((Object(i.a)(this, r), !t.state.busy)) {
                  var e = t.state.page;
                  t.setState(
                    { busy: !0 },
                    Object(f.a)(
                      h.a.mark(function n() {
                        var i, r;
                        return h.a.wrap(
                          function (n) {
                            for (;;)
                              switch ((n.prev = n.next)) {
                                case 0:
                                  return (
                                    (i = t.isNovel() ? zt.b : zt.e),
                                    (n.prev = 1),
                                    (n.next = 4),
                                    i(t.props.client, t.props.rootCommentId, e)
                                  );
                                case 4:
                                  (r = n.sent),
                                    t.setState({
                                      childComments: {
                                        comments: r.comments.reverse(),
                                        hasNext: r.hasNext,
                                      },
                                      page: 2,
                                    });
                                case 6:
                                  return (
                                    (n.prev = 6),
                                    t.setState({ busy: !1 }),
                                    n.finish(6)
                                  );
                                case 9:
                                case "end":
                                  return n.stop();
                              }
                          },
                          n,
                          null,
                          [[1, , 6, 9]]
                        );
                      })
                    )
                  );
                }
              }.bind(this)
            ),
            Object(x.a)(
              Object(O.a)(t),
              "handleClickShowMore",
              function () {
                Object(i.a)(this, r);
                var e = t.state;
                if (!e.busy) {
                  var n = t.state.page;
                  t.setState(
                    { busy: !0 },
                    Object(f.a)(
                      h.a.mark(function r() {
                        var o,
                          c,
                          a,
                          s,
                          l = this;
                        return h.a.wrap(
                          function (r) {
                            for (;;)
                              switch ((r.prev = r.next)) {
                                case 0:
                                  return (
                                    (r.prev = 0),
                                    (o = t.isNovel() ? zt.b : zt.e),
                                    (r.next = 4),
                                    o(t.props.client, t.props.rootCommentId, n)
                                  );
                                case 4:
                                  (c = r.sent),
                                    (a = c.comments
                                      .reverse()
                                      .concat(e.childComments.comments)),
                                    (s = Array.from(
                                      new Map(
                                        a.map(
                                          function (t) {
                                            return (
                                              Object(i.a)(this, l), [t.id, t]
                                            );
                                          }.bind(this)
                                        )
                                      ).values()
                                    )),
                                    t.setState({
                                      childComments: {
                                        comments: s,
                                        hasNext: c.hasNext,
                                      },
                                      page: e.page + 1,
                                    });
                                case 8:
                                  return (
                                    (r.prev = 8),
                                    t.setState({ busy: !1 }),
                                    r.finish(8)
                                  );
                                case 11:
                                case "end":
                                  return r.stop();
                              }
                          },
                          r,
                          this,
                          [[0, , 8, 11]]
                        );
                      })
                    )
                  );
                }
              }.bind(this)
            ),
            Object(x.a)(
              Object(O.a)(t),
              "handleClickShowAll",
              function () {
                var e = this;
                Object(i.a)(this, r),
                  t.setState(
                    {
                      childComments: { comments: [], hasNext: !1 },
                      page: 1,
                      permaLinkReplyMode: !1,
                    },
                    function () {
                      Object(i.a)(this, e), t.handleClickShowMore();
                    }.bind(this)
                  );
              }.bind(this)
            ),
            Object(x.a)(
              Object(O.a)(t),
              "makeClickDeleteCommentHandler",
              function (e) {
                return (
                  Object(i.a)(this, r),
                  (function () {
                    var n = Object(f.a)(
                      h.a.mark(function n(r) {
                        var o,
                          c,
                          a,
                          s = this;
                        return h.a.wrap(
                          function (n) {
                            for (;;)
                              switch ((n.prev = n.next)) {
                                case 0:
                                  if (!confirm(e)) {
                                    n.next = 7;
                                    break;
                                  }
                                  return (
                                    (o = r.currentTarget.dataset.commentId),
                                    (c = t.isNovel()),
                                    (a = c ? ne : ee),
                                    (n.next = 6),
                                    a(
                                      t.props.client,
                                      c ? t.props.novelId : t.props.illustId,
                                      o
                                    )
                                  );
                                case 6:
                                  t.setState(
                                    function (t) {
                                      var e = this;
                                      Object(i.a)(this, s);
                                      var n = t.childComments;
                                      return {
                                        childComments: {
                                          comments: n.comments.filter(
                                            function (t) {
                                              return (
                                                Object(i.a)(this, e), t.id !== o
                                              );
                                            }.bind(this)
                                          ),
                                          hasNext: n.hasNext,
                                        },
                                      };
                                    }.bind(this)
                                  );
                                case 7:
                                case "end":
                                  return n.stop();
                              }
                          },
                          n,
                          this
                        );
                      })
                    );
                    return function (t) {
                      return n.apply(this, arguments);
                    };
                  })()
                );
              }.bind(this)
            ),
            t
          );
        }
        return (
          Object(p.a)(n, [
            {
              key: "componentDidMount",
              value: function () {
                null !== this.props.permaLinkReplyComments &&
                  this.setState({
                    childComments: {
                      comments: this.props.permaLinkReplyComments.comments,
                      hasNext: this.props.permaLinkReplyComments.hasNext,
                    },
                    permaLinkReplyMode: !0,
                  });
              },
            },
            {
              key: "componentDidUpdate",
              value: function (t) {
                var e = this,
                  n = this.props.repliedComment;
                n !== t.repliedComment &&
                  null !== n &&
                  this.setState(
                    function (t) {
                      return (
                        Object(i.a)(this, e),
                        {
                          childComments: {
                            comments:
                              null !== t.childComments
                                ? t.childComments.comments.concat(n)
                                : [n],
                            hasNext:
                              null !== t.childComments
                                ? t.childComments.hasNext
                                : this.props.hasReply,
                          },
                        }
                      );
                    }.bind(this)
                  );
              },
            },
            {
              key: "render",
              value: function () {
                var t = this,
                  e = this.props,
                  n = e.hasReply,
                  r = e.disabledForm,
                  c = e.onClickReply,
                  a = this.state,
                  s = a.childComments,
                  l = a.permaLinkReplyMode,
                  u = a.busy;
                return Object(o.jsx)("div", {
                  children: Object(o.jsx)(d.d, {
                    children: function (e) {
                      var a = this;
                      return (
                        Object(i.a)(this, t),
                        null === s && n
                          ? Object(o.jsx)(ce, {
                              busy: u,
                              onClick: this.handleClickShowReplies,
                              children: e("瓦붶에�믦쫳��"),
                            })
                          : null !== s &&
                            Object(o.jsxs)(o.Fragment, {
                              children: [
                                !l &&
                                  s.hasNext &&
                                  Object(o.jsx)(ce, {
                                    busy: u,
                                    onClick: this.handleClickShowMore,
                                    children: e("餓ε뎺��퓭岳▲굮誤뗣굥"),
                                  }),
                                Object(o.jsx)("ul", {
                                  className: ie.list,
                                  children: s.comments.map(
                                    function (t) {
                                      return (
                                        Object(i.a)(this, a),
                                        Object(o.jsx)(
                                          "li",
                                          {
                                            className: ie.item,
                                            children: Object(o.jsx)(
                                              Qt,
                                              {
                                                creatorUserId: this.props
                                                  .creatorUserId,
                                                viewerUserId: this.props
                                                  .viewerUserId,
                                                comment: t,
                                                disabledForm: r,
                                                child: !0,
                                                onDeleteComment: this.makeClickDeleteCommentHandler(
                                                  e(
                                                    "�녈깳�녈깉�믣뎷�ㅳ걮�얇걲�뗰폕"
                                                  )
                                                ),
                                                onClickReply: c,
                                              },
                                              t.id
                                            ),
                                          },
                                          t.id
                                        )
                                      );
                                    }.bind(this)
                                  ),
                                }),
                                l &&
                                  s.hasNext &&
                                  Object(o.jsx)(ce, {
                                    busy: !1,
                                    onClick: this.handleClickShowAll,
                                    children: e("�쇻겧�╉겗瓦붶에�믦쫳��"),
                                  }),
                              ],
                            })
                      );
                    }.bind(this),
                  }),
                });
              },
            },
            {
              key: "isNovel",
              value: function () {
                return !!this.props.novelId;
              },
            },
          ]),
          n
        );
      })(s.a.Component);
      function ce(t) {
        return Object(o.jsxs)("div", {
          className: ie.showMoreButtonContainer,
          children: [
            Object(o.jsx)("button", {
              type: "button",
              onClick: t.onClick,
              className: Y()(
                ie.showMoreButton,
                Object(x.a)({}, ie.busy, t.busy)
              ),
              children: t.children,
            }),
            t.busy &&
              Object(o.jsx)("div", {
                className: ie.loading,
                children: Object(o.jsx)(Tt.a, { loading: !0, size: 24 }),
              }),
          ],
        });
      }
      var ae = Object(y.connect)(function (t) {
          return { client: Object(te.d)(t) };
        })(oe),
        se = n(1386);
      function le(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(g.a)(t);
          if (e) {
            var r = Object(g.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(v.a)(this, n);
        };
      }
      var ue = (function (t) {
          Object(m.a)(n, t);
          var e = le(n);
          function n() {
            var t,
              r = this;
            Object(j.a)(this, n);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(x.a)(Object(O.a)(t), "state", {
                commentValue: "",
                replyFormShow: !1,
                repliedComment: null,
              }),
              Object(x.a)(Object(O.a)(t), "replyParentId", "0"),
              Object(x.a)(
                Object(O.a)(t),
                "handleClickReply",
                function (e) {
                  Object(i.a)(this, r),
                    (t.replyParentId = e),
                    t.setState({ replyFormShow: !0 });
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "handleInputChange",
                function (e) {
                  Object(i.a)(this, r), t.setState({ commentValue: e });
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "makeSubmitReplyHandler",
                function (e) {
                  return (
                    Object(i.a)(this, r),
                    (function () {
                      var n = Object(f.a)(
                        h.a.mark(function n(i) {
                          var r, o, c, a, s, l, u, d, b, f, j, p;
                          return h.a.wrap(
                            function (n) {
                              for (;;)
                                switch ((n.prev = n.next)) {
                                  case 0:
                                    if (
                                      ((r = t.props),
                                      (o = r.client),
                                      (c = r.novelId),
                                      (a = r.illustId),
                                      (s = r.creatorUserId),
                                      (l = r.profileImage),
                                      !r.notAuthorized)
                                    ) {
                                      n.next = 4;
                                      break;
                                    }
                                    return alert(e), n.abrupt("return");
                                  case 4:
                                    return (
                                      (n.prev = 4),
                                      (u = Object(O.a)(t)),
                                      (d = u.replyParentId),
                                      (b = t.isNovel()),
                                      (f = b ? zt.j : zt.i),
                                      (n.next = 10),
                                      f(o, b ? c : a, s, i, d)
                                    );
                                  case 10:
                                    (j = n.sent),
                                      (p = {
                                        userId: j.user_id,
                                        userName: j.user_name,
                                        img: l,
                                        id: "".concat(j.comment_id),
                                        comment: j.comment,
                                        stampId: j.stamp_id,
                                        commentParentId: d,
                                        commentDate: Object(
                                          Rt.default
                                        )().format(),
                                        commentUserId: j.user_id,
                                        editable: !0,
                                      }),
                                      t.setState({
                                        repliedComment: p,
                                        commentValue: "",
                                      });
                                  case 13:
                                    return (
                                      (n.prev = 13),
                                      t.setState({ replyFormShow: !1 }),
                                      n.finish(13)
                                    );
                                  case 16:
                                  case "end":
                                    return n.stop();
                                }
                            },
                            n,
                            null,
                            [[4, , 13, 16]]
                          );
                        })
                      );
                      return function (t) {
                        return n.apply(this, arguments);
                      };
                    })()
                  );
                }.bind(this)
              ),
              Object(x.a)(
                Object(O.a)(t),
                "makeSubmitStampHandler",
                function (e) {
                  var n = this;
                  return (
                    Object(i.a)(this, r),
                    function (r) {
                      Object(i.a)(this, n);
                      var o = r.id,
                        c = t.props,
                        a = c.client,
                        s = c.novelId,
                        l = c.illustId,
                        u = c.creatorUserId,
                        d = c.profileImage;
                      if (c.notAuthorized) alert(e);
                      else {
                        var b = Object(O.a)(t).replyParentId;
                        t.setState(
                          { replyFormShow: !1 },
                          Object(f.a)(
                            h.a.mark(function e() {
                              var n, i, r, c;
                              return h.a.wrap(function (e) {
                                for (;;)
                                  switch ((e.prev = e.next)) {
                                    case 0:
                                      return (
                                        (n = t.isNovel()),
                                        (i = n ? zt.l : zt.k),
                                        (e.next = 4),
                                        i(a, n ? s : l, u, o, b)
                                      );
                                    case 4:
                                      (r = e.sent),
                                        (c = {
                                          userId: r.user_id,
                                          userName: r.user_name,
                                          img: d,
                                          id: "".concat(r.comment_id),
                                          comment: r.comment,
                                          stampId: r.stamp_id,
                                          commentParentId: b,
                                          commentDate: Object(
                                            Rt.default
                                          )().format(),
                                          commentUserId: r.user_id,
                                          editable: !0,
                                        }),
                                        t.setState({ repliedComment: c });
                                    case 7:
                                    case "end":
                                      return e.stop();
                                  }
                              }, e);
                            })
                          )
                        );
                      }
                    }.bind(this)
                  );
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(p.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this,
                    e = this.props,
                    n = e.creatorUserId,
                    r = e.viewerUserId,
                    c = e.illustId,
                    a = e.novelId,
                    s = e.comment,
                    l = e.isMyself,
                    u = e.disabledForm,
                    b = e.profileImage,
                    h = e.onDeleteComment,
                    f = e.permaLinkReplyComments,
                    j = this.state,
                    p = j.commentValue,
                    O = j.replyFormShow,
                    m = j.repliedComment;
                  return Object(o.jsx)("div", {
                    children: Object(o.jsxs)("div", {
                      children: [
                        Object(o.jsx)(Qt, {
                          creatorUserId: n,
                          viewerUserId: r,
                          comment: s,
                          disabledForm: u,
                          onClickReply: this.handleClickReply,
                          onDeleteComment: h,
                        }),
                        Object(o.jsxs)("div", {
                          className: se.indent,
                          children: [
                            c
                              ? Object(o.jsx)(ae, {
                                  hasReply: s.hasReplies,
                                  rootCommentId: s.id,
                                  creatorUserId: n,
                                  viewerUserId: r,
                                  illustId: c,
                                  isMyself: l,
                                  disabledForm: u,
                                  repliedComment: m,
                                  permaLinkReplyComments: f,
                                  onClickReply: this.handleClickReply,
                                })
                              : Object(o.jsx)(ae, {
                                  hasReply: s.hasReplies,
                                  rootCommentId: s.id,
                                  creatorUserId: n,
                                  viewerUserId: r,
                                  novelId: a,
                                  isMyself: l,
                                  disabledForm: u,
                                  repliedComment: m,
                                  permaLinkReplyComments: f,
                                  onClickReply: this.handleClickReply,
                                }),
                            O &&
                              Object(o.jsx)(d.f, {
                                ns: [d.m.error.ns, d.m.action.ns],
                                children: function (e) {
                                  return (
                                    Object(i.a)(this, t),
                                    Object(o.jsx)(kt, {
                                      value: p,
                                      placeholder: e(
                                        d.m.action.comment.reply_placeholder
                                      ),
                                      autoFocus: !0,
                                      profileImage: b,
                                      onChange: this.handleInputChange,
                                      onSubmit: this.makeSubmitReplyHandler(
                                        e(
                                          d.m.error
                                            .require_mail_confirmation_to_comment
                                        )
                                      ),
                                      onSubmitStamp: this.makeSubmitStampHandler(
                                        e(
                                          d.m.error
                                            .require_mail_confirmation_to_comment
                                        )
                                      ),
                                    })
                                  );
                                }.bind(this),
                              }),
                          ],
                        }),
                      ],
                    }),
                  });
                },
              },
              {
                key: "isNovel",
                value: function () {
                  return !!this.props.novelId;
                },
              },
            ]),
            n
          );
        })(s.a.Component),
        de = Object(y.connect)(function (t) {
          return { client: Object(te.d)(t) };
        })(ue),
        be = n(1153),
        he = n(1387);
      function fe(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function je(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? fe(Object(n), !0).forEach(function (e) {
                Object(x.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : fe(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      var pe = s.a.memo(Oe);
      function Oe(t) {
        var e = this,
          n = t.creatorUserId,
          a = t.disabledForm,
          s = t.isMyself,
          l = t.notAuthorized,
          b = t.profileImage,
          j = t.submittedComment,
          p = t.viewerUserId,
          O = t.illustId,
          m = t.novelId,
          v = void 0 !== m,
          g = Object(d.p)([d.m.action.ns, d.m.work.ns]),
          y = Object(r.a)(g, 1)[0],
          w = Object(c.j)(null),
          k = Object(r.a)(w, 2),
          C = k[0],
          I = k[1],
          _ = Object(c.j)(0),
          S = Object(r.a)(_, 2),
          P = S[0],
          D = S[1],
          T = Object(c.j)(null),
          R = Object(r.a)(T, 2),
          z = R[0],
          N = R[1],
          M = (function () {
            var t = this,
              e = Object(c.j)(),
              n = Object(r.a)(e, 2),
              o = n[0],
              a = n[1],
              s = Object(c.j)(),
              l = Object(r.a)(s, 2),
              d = l[0],
              b = l[1];
            return [
              Object(c.a)(
                function (e) {
                  Object(i.a)(this, t);
                  var n = Object(be.unstable_getCurrentPriorityLevel)();
                  Object(f.a)(
                    h.a.mark(function t() {
                      var r,
                        o,
                        c = this;
                      return h.a.wrap(
                        function (t) {
                          for (;;)
                            switch ((t.prev = t.next)) {
                              case 0:
                                return (
                                  (t.prev = 0),
                                  u.a.unstable_discreteUpdates(
                                    function () {
                                      Object(i.a)(this, c), (r = e()) && a(r);
                                    }.bind(this)
                                  ),
                                  (t.next = 4),
                                  r
                                );
                              case 4:
                                if ((o = t.sent)) {
                                  t.next = 7;
                                  break;
                                }
                                return t.abrupt("return");
                              case 7:
                                u.a.unstable_discreteUpdates(
                                  function () {
                                    var t = this;
                                    Object(i.a)(this, c),
                                      Object(be.unstable_runWithPriority)(
                                        n > be.unstable_NormalPriority
                                          ? be.unstable_NormalPriority
                                          : n,
                                        function () {
                                          Object(i.a)(this, t), a(void 0), o();
                                        }.bind(this)
                                      );
                                  }.bind(this)
                                ),
                                  (t.next = 13);
                                break;
                              case 10:
                                (t.prev = 10),
                                  (t.t0 = t.catch(0)),
                                  u.a.unstable_discreteUpdates(
                                    function () {
                                      Object(i.a)(this, c),
                                        t.t0 instanceof Error && b(t.t0),
                                        a(void 0);
                                    }.bind(this)
                                  );
                              case 13:
                              case "end":
                                return t.stop();
                            }
                        },
                        t,
                        this,
                        [[0, 10]]
                      );
                    })
                  )();
                }.bind(this),
                []
              ),
              !!o,
              d,
            ];
          })(),
          q = Object(r.a)(M, 2),
          U = q[0],
          E = q[1],
          L = Object(Dt.b)(te.d),
          A = Object(Pt.f)(),
          F = Object(Pt.g)(),
          B = Object(c.i)(null),
          W = Object(c.g)(
            function () {
              return (
                Object(i.a)(this, e),
                St.a.parse(F.search, { ignoreQueryPrefix: !0 }).comment_id
              );
            }.bind(this),
            [F.search]
          ),
          H = Object(c.i)(!1),
          V = Object(c.i)(!1);
        Object(c.d)(
          function () {
            var t = this;
            Object(i.a)(this, e);
            var n = H.current,
              r = B.current;
            if (void 0 !== W && null === z) {
              V.current = !0;
              var o = v ? zt.a : zt.d;
              U(
                Object(f.a)(
                  h.a.mark(function t() {
                    var e,
                      n = this;
                    return h.a.wrap(
                      function (t) {
                        for (;;)
                          switch ((t.prev = t.next)) {
                            case 0:
                              return (t.next = 2), o(L, W);
                            case 2:
                              return (
                                (e = t.sent),
                                t.abrupt(
                                  "return",
                                  function () {
                                    Object(i.a)(this, n),
                                      I({
                                        comments: e.root.comments,
                                        hasNext: e.root.hasNext,
                                      }),
                                      D(0),
                                      N({
                                        comments: e.child.comments,
                                        hasNext: e.child.hasNext,
                                      });
                                  }.bind(this)
                                )
                              );
                            case 4:
                            case "end":
                              return t.stop();
                          }
                      },
                      t,
                      this
                    );
                  })
                )
              ),
                n ||
                  setTimeout(
                    function () {
                      Object(i.a)(this, t),
                        Object(It.g)(r, { block: "start", offset: -14 });
                    }.bind(this),
                    1e3
                  );
            }
          }.bind(this),
          [L, W, v, z, U]
        ),
          Object(c.d)(
            function () {
              var t = this;
              if ((Object(i.a)(this, e), !V.current)) {
                var n = B.current;
                "#comment" === F.hash &&
                  (A.replace(je(je({}, F), {}, { hash: "" })),
                  setTimeout(
                    function () {
                      Object(i.a)(this, t),
                        Object(It.g)(n, { block: "start", offset: -14 });
                    }.bind(this),
                    1e3
                  ));
              }
            }.bind(this),
            [A, F]
          ),
          Object(c.d)(
            function () {
              Object(i.a)(this, e),
                V.current ||
                  U(
                    Object(f.a)(
                      h.a.mark(function t() {
                        var e,
                          n,
                          r,
                          o = this;
                        return h.a.wrap(
                          function (t) {
                            for (;;)
                              switch ((t.prev = t.next)) {
                                case 0:
                                  return (
                                    (e = v ? zt.c : zt.f),
                                    (n = 3),
                                    (t.next = 4),
                                    e(L, v ? m : O, 0, n)
                                  );
                                case 4:
                                  return (
                                    (r = t.sent),
                                    t.abrupt(
                                      "return",
                                      function () {
                                        Object(i.a)(this, o),
                                          I(r),
                                          D(n),
                                          N(null);
                                      }.bind(this)
                                    )
                                  );
                                case 6:
                                case "end":
                                  return t.stop();
                              }
                          },
                          t,
                          this
                        );
                      })
                    )
                  );
            }.bind(this),
            [L, W, O, v, m, U]
          ),
          Object(c.d)(
            function () {
              var t = this;
              Object(i.a)(this, e),
                V.current ||
                  (null !== j &&
                    (N(null),
                    I(
                      function (e) {
                        return (
                          Object(i.a)(this, t),
                          e
                            ? je(
                                je({}, e),
                                {},
                                { comments: [j].concat(e.comments) }
                              )
                            : e
                        );
                      }.bind(this)
                    )));
            }.bind(this),
            [j]
          ),
          Object(c.d)(
            function () {
              Object(i.a)(this, e), (V.current = !1);
            }.bind(this)
          ),
          Object(c.d)(
            function () {
              var t = this;
              return (
                Object(i.a)(this, e),
                (H.current = !0),
                function () {
                  Object(i.a)(this, t), (H.current = !1);
                }.bind(this)
              );
            }.bind(this),
            []
          );
        var G = function () {
            Object(i.a)(this, e),
              E ||
                U(
                  Object(f.a)(
                    h.a.mark(function t() {
                      var e,
                        n,
                        r,
                        o,
                        c = this;
                      return h.a.wrap(
                        function (t) {
                          for (;;)
                            switch ((t.prev = t.next)) {
                              case 0:
                                return (
                                  (e = v ? zt.c : zt.f),
                                  (t.next = 3),
                                  e(L, v ? m : O, P, 50)
                                );
                              case 3:
                                return (
                                  (n = t.sent),
                                  (r = C.comments.concat(n.comments)),
                                  (o = Array.from(
                                    new Map(
                                      r.map(
                                        function (t) {
                                          return (
                                            Object(i.a)(this, c), [t.id, t]
                                          );
                                        }.bind(this)
                                      )
                                    ).values()
                                  )),
                                  t.abrupt(
                                    "return",
                                    function () {
                                      var t = this;
                                      Object(i.a)(this, c),
                                        I({ comments: o, hasNext: n.hasNext }),
                                        D(
                                          function (e) {
                                            return Object(i.a)(this, t), e + 50;
                                          }.bind(this)
                                        );
                                    }.bind(this)
                                  )
                                );
                              case 7:
                              case "end":
                                return t.stop();
                            }
                        },
                        t,
                        this
                      );
                    })
                  )
                );
          }.bind(this),
          Z = function () {
            var t = this;
            Object(i.a)(this, e);
            var n = St.a.parse(F.search, { ignoreQueryPrefix: !0 }),
              r = n.comment_id,
              o = Object(it.a)(n, ["comment_id"]);
            U(
              function () {
                Object(i.a)(this, t),
                  void 0 !== r &&
                    A.push({
                      search: St.a.stringify(o, { addQueryPrefix: !0 }),
                    }),
                  I(null),
                  D(0),
                  N(null);
              }.bind(this)
            );
          }.bind(this),
          X = function (t) {
            if (
              (Object(i.a)(this, e),
              confirm(y(d.m.action.comment.confirm_delete_comment)))
            ) {
              var n = t.currentTarget.dataset.commentId,
                r = v ? ne : ee;
              U(
                Object(f.a)(
                  h.a.mark(function t() {
                    var e = this;
                    return h.a.wrap(
                      function (t) {
                        for (;;)
                          switch ((t.prev = t.next)) {
                            case 0:
                              return (t.next = 2), r(L, v ? m : O, n);
                            case 2:
                              return t.abrupt(
                                "return",
                                function () {
                                  var t = this;
                                  Object(i.a)(this, e),
                                    I(
                                      function (e) {
                                        var r = this;
                                        return (
                                          Object(i.a)(this, t),
                                          {
                                            comments: e.comments.filter(
                                              function (t) {
                                                return (
                                                  Object(i.a)(this, r),
                                                  t.id !== n
                                                );
                                              }.bind(this)
                                            ),
                                            hasNext: e.hasNext,
                                          }
                                        );
                                      }.bind(this)
                                    );
                                }.bind(this)
                              );
                            case 3:
                            case "end":
                              return t.stop();
                          }
                      },
                      t,
                      this
                    );
                  })
                )
              );
            }
          }.bind(this);
        return Object(o.jsx)("div", {
          className: Y()(he.wrapper, he.spa),
          ref: B,
          children:
            null === C
              ? Object(o.jsx)("div", {
                  className: he.loadingArea,
                  children: Object(o.jsx)(Tt.a, { loading: !0 }),
                })
              : 0 === C.comments.length
              ? Object(o.jsx)("div", {
                  className: he.noItem,
                  children: y(d.m.work.info_by_user.no_comment_yet),
                })
              : Object(o.jsxs)(o.Fragment, {
                  children: [
                    Object(o.jsx)("ul", {
                      className: he.rootList,
                      children: C.comments.map(
                        function (t) {
                          return (
                            Object(i.a)(this, e),
                            Object(o.jsx)(
                              "li",
                              {
                                className: he.rootItem,
                                children: Object(o.jsx)(de, {
                                  comment: t,
                                  creatorUserId: n,
                                  viewerUserId: p,
                                  illustId: O,
                                  novelId: m,
                                  isMyself: s,
                                  disabledForm: a,
                                  notAuthorized: l,
                                  profileImage: b,
                                  onDeleteComment: X,
                                  permaLinkReplyComments: z,
                                }),
                              },
                              t.id
                            )
                          );
                        }.bind(this)
                      ),
                    }),
                    Object(o.jsx)("div", {
                      className: he.buttonArea,
                      children:
                        C.hasNext &&
                        (z
                          ? Object(o.jsx)("button", {
                              type: "button",
                              onClick: Z,
                              className: he.showMoreButton,
                              children: y(d.m.action.link.see_all_comment),
                            })
                          : E
                          ? Object(o.jsx)("div", {
                              className: he.loadingShowMore,
                              children: Object(o.jsx)(Tt.a, { loading: !0 }),
                            })
                          : Object(o.jsx)("button", {
                              type: "button",
                              onClick: G,
                              className: Y()(
                                he.showMoreButton,
                                Object(x.a)({}, he.busy, E)
                              ),
                              children: y(d.m.action.link.see_more_comment),
                            })),
                    }),
                  ],
                }),
        });
      }
      var me = n(812),
        ve = n(1388),
        ge = s.a.forwardRef(function (t, e) {
          var n = this,
            a = t.creatorUserId,
            l = t.viewerUserId,
            b = t.isMyself,
            h = t.disabledForm,
            f = t.notAuthorized,
            j = t.profileImage,
            p = t.illustId,
            O = t.novelId,
            m = t.onSubmit,
            v = t.onSubmitStamp,
            g = Object(d.p)([d.m.work.ns, d.m.error.ns]),
            x = Object(r.a)(g, 1)[0],
            y = Object(c.j)(null),
            w = Object(r.a)(y, 2),
            k = w[0],
            C = w[1],
            I = Object(c.j)(""),
            _ = Object(r.a)(I, 2),
            S = _[0],
            P = _[1];
          Object(c.e)(
            e,
            function () {
              return (
                Object(i.a)(this, n),
                {
                  injectComment: function (t, e) {
                    var n = this;
                    u.a.unstable_batchedUpdates(
                      function () {
                        Object(i.a)(this, n), e && P(""), C(t);
                      }.bind(this)
                    );
                  },
                }
              );
            }.bind(this),
            []
          );
          var D = function (t) {
              var e = this;
              Object(i.a)(this, n),
                f
                  ? alert(x(d.m.error.require_mail_confirmation_to_comment))
                  : u.a.unstable_batchedUpdates(
                      function () {
                        Object(i.a)(this, e), m(t), P("");
                      }.bind(this)
                    );
            }.bind(this),
            T = !!O,
            R = T ? O : p;
          return Object(o.jsxs)("div", {
            children: [
              Object(o.jsx)("h2", {
                className: ve.spaLabel,
                children: x(d.m.work.info_by_user.comment),
              }),
              !h &&
                Object(o.jsx)(kt, {
                  value: S,
                  onChange: P,
                  profileImage: j,
                  onSubmit: D,
                  onSubmitStamp: v,
                  shortcutVk: "c",
                }),
              Object(o.jsx)(s.a.Suspense, {
                fallback: Object(o.jsx)(me.a, {}),
                children: T
                  ? Object(o.jsx)(pe, {
                      creatorUserId: a,
                      viewerUserId: l,
                      isMyself: b,
                      disabledForm: h,
                      notAuthorized: f,
                      profileImage: j,
                      novelId: R,
                      submittedComment: k,
                    })
                  : Object(o.jsx)(pe, {
                      creatorUserId: a,
                      viewerUserId: l,
                      isMyself: b,
                      disabledForm: h,
                      notAuthorized: f,
                      profileImage: j,
                      illustId: R,
                      submittedComment: k,
                    }),
              }),
            ],
          });
        });
    },
    1457: function (t, e, n) {
      "use strict";
      n(28), n(94), n(53), n(103), n(169);
      var i = n(1),
        r = n(7),
        o = n.n(r),
        c = (n(39), n(12)),
        a = n(34),
        s = n(37),
        l = n(24),
        u = n(36),
        d = n(45),
        b = n(30),
        h = n(4),
        f = n(0),
        j = n(40),
        p = n(6),
        O = n.n(p),
        m = n(51),
        v = n(347),
        g = n(390),
        x = n(87),
        y = n(595),
        w = n(5),
        k = n(1283),
        C = n(8),
        I = n(267),
        _ = n(2),
        S = O.a.memo(P);
      function P(t) {
        var e = Object(w.p)(w.m.work.ns),
          n = Object(C.a)(e, 1)[0];
        return Object(f.jsxs)(D, {
          children: [
            Object(f.jsx)(I.a, {
              disabled: t.disabled,
              checked: t.checked,
              onChange: t.onChange,
              children: n(w.m.work.info_by_user.allow_other_users_to_edit_tag),
            }),
            Object(f.jsx)(T, {
              children: n(
                w.m.work.info_by_user.tag_edit_attention_of_your_work
              ),
            }),
          ],
        });
      }
      var D = _.default.div.withConfig({ componentId: "sc-1cuo2nq-0" })([
          "margin:8px 0;padding-top:8px;line-height:1.5;",
        ]),
        T = _.default.div.withConfig({ componentId: "sc-1cuo2nq-1" })([
          "color:#999;max-width:340px;margin:8px 0;",
        ]),
        R = n(160),
        z = n(1059);
      function N(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(b.a)(t);
          if (e) {
            var r = Object(b.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(d.a)(this, n);
        };
      }
      var M = (function (t) {
          Object(u.a)(n, t);
          var e = N(n);
          function n() {
            var t,
              r = this;
            Object(a.a)(this, n);
            for (var o = arguments.length, c = new Array(o), s = 0; s < o; s++)
              c[s] = arguments[s];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(h.a)(Object(l.a)(t), "state", {
                value: "",
                showInput: !1,
              }),
              Object(h.a)(
                Object(l.a)(t),
                "handleTagChange",
                function (e) {
                  Object(i.a)(this, r), t.setState({ value: e });
                }.bind(this)
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleTagAddClick",
                function () {
                  Object(i.a)(this, r), t.setState({ showInput: !0 });
                }.bind(this)
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleTagAdd",
                function (e) {
                  Object(i.a)(this, r), e.preventDefault();
                  var n = t.props.onTagAdd;
                  "" !== t.state.value &&
                    (n(t.state.value),
                    t.setState({ showInput: !1, value: "" }));
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(s.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this;
                  return Object(f.jsx)(q, {
                    onSubmit: this.handleTagAdd,
                    children: Object(f.jsx)(w.d, {
                      children: function (e) {
                        return (
                          Object(i.a)(this, t),
                          this.state.showInput && !this.props.disabled
                            ? Object(f.jsx)(z.b, {
                                value: this.state.value,
                                onChange: this.handleTagChange,
                                placeholder: e("�욍궛�믦옙��"),
                                maxLength: 30,
                                autoFocus: !0,
                                showMaxLength: !0,
                              })
                            : Object(f.jsx)(R.a, {
                                disabled: this.props.disabled,
                                linkLike: !0,
                                onClick: this.handleTagAddClick,
                                children: e("�욍궛�믦옙��"),
                              })
                        );
                      }.bind(this),
                    }),
                  });
                },
              },
            ]),
            n
          );
        })(O.a.PureComponent),
        q = _.default.form.withConfig({ componentId: "sc-1uhv1oa-0" })([
          "flex:none;display:flex;flex-flow:column;padding:8px 24px;height:32px;> button{flex:auto;}",
        ]),
        U = (n(52), n(158), n(309)),
        E = n(353),
        L = n(501),
        A = function (t) {
          Object(i.a)(this, void 0);
          var e = t.size,
            n = t.closed,
            r = t.darkGray,
            o = t.currentColor;
          return Object(f.jsxs)(F, {
            viewBox: "0 3 16 18",
            width: (16 * e) / 18,
            height: e,
            darkGray: r,
            currentColor: o,
            children: [
              Object(f.jsx)(B, {
                d:
                  "M11,11 L11,6 C11,4.34314575 9.65685425,3 8,3 C6.34314575,3 5,4.34314575 5,6 L5,8 L2,8 L2,6\nC2,2.6862915 4.6862915,0 8,0 C11.3137085,0 14,2.6862915 14,6 L14,11 L11,11 Z",
                transform: n ? "translate(0, 3)" : void 0,
              }),
              Object(f.jsx)("rect", {
                x: 0,
                y: 11,
                width: 16,
                height: 10,
                rx: 2,
              }),
            ],
          });
        }.bind(void 0);
      A.defaultProps = { size: 18, closed: !1 };
      var F = _.default.svg.withConfig({ componentId: "mffm4t-0" })(
          ["stroke:none;overflow:visible;fill:", ";"],
          function (t) {
            return (
              Object(i.a)(this, void 0),
              t.currentColor
                ? "currentColor"
                : t.darkGray
                ? t.theme.text.default
                : t.theme.text.mediumGray
            );
          }.bind(void 0)
        ),
        B = _.default.path.withConfig({ componentId: "mffm4t-1" })([
          "transition:transform 0.3s;",
        ]),
        W = n(14),
        H = n(3),
        V = n(26),
        G = n(77);
      function Z(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(b.a)(t);
          if (e) {
            var r = Object(b.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(d.a)(this, n);
        };
      }
      var Y = (function (t) {
          Object(u.a)(n, t);
          var e = Z(n);
          function n() {
            var t;
            Object(a.a)(this, n);
            for (var i = arguments.length, r = new Array(i), s = 0; s < i; s++)
              r[s] = arguments[s];
            return (
              (t = e.call.apply(e, [this].concat(r))),
              Object(h.a)(
                Object(l.a)(t),
                "handleLockClick",
                Object(c.a)(
                  o.a.mark(function e() {
                    var n;
                    return o.a.wrap(
                      function (e) {
                        for (;;)
                          switch ((e.prev = e.next)) {
                            case 0:
                              return (
                                (n = t.props.tag.locked ? U.s : U.n),
                                (e.prev = 1),
                                (e.next = 4),
                                t.props.dispatch(
                                  n(
                                    t.props.workType,
                                    t.props.id,
                                    t.props.tag.tag
                                  )
                                )
                              );
                            case 4:
                              e.next = 9;
                              break;
                            case 6:
                              (e.prev = 6),
                                (e.t0 = e.catch(1)),
                                t.props.onError(e.t0);
                            case 9:
                            case "end":
                              return e.stop();
                          }
                      },
                      e,
                      null,
                      [[1, 6]]
                    );
                  })
                )
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleDeleteClick",
                Object(c.a)(
                  o.a.mark(function e() {
                    return o.a.wrap(
                      function (e) {
                        for (;;)
                          switch ((e.prev = e.next)) {
                            case 0:
                              return (
                                (e.prev = 0),
                                (e.next = 3),
                                t.props.dispatch(
                                  U.f(
                                    t.props.workType,
                                    t.props.id,
                                    t.props.tag.tag
                                  )
                                )
                              );
                            case 3:
                              e.next = 8;
                              break;
                            case 5:
                              (e.prev = 5),
                                (e.t0 = e.catch(0)),
                                t.props.onError(e.t0);
                            case 8:
                            case "end":
                              return e.stop();
                          }
                      },
                      e,
                      null,
                      [[0, 5]]
                    );
                  })
                )
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleRestoreClick",
                Object(c.a)(
                  o.a.mark(function e() {
                    return o.a.wrap(
                      function (e) {
                        for (;;)
                          switch ((e.prev = e.next)) {
                            case 0:
                              return (
                                (e.prev = 0),
                                (e.next = 3),
                                t.props.dispatch(
                                  U.q(
                                    t.props.workType,
                                    t.props.id,
                                    t.props.tag.key
                                  )
                                )
                              );
                            case 3:
                              e.next = 8;
                              break;
                            case 5:
                              (e.prev = 5),
                                (e.t0 = e.catch(0)),
                                t.props.onError(e.t0);
                            case 8:
                            case "end":
                              return e.stop();
                          }
                      },
                      e,
                      null,
                      [[0, 5]]
                    );
                  })
                )
              ),
              t
            );
          }
          return (
            Object(s.a)(n, [
              {
                key: "render",
                value: function () {
                  var t = this.props,
                    e = t.tagsLength,
                    n = t.busy,
                    i = t.tag,
                    r = t.authorId,
                    o = t.isLocked,
                    c = t.isSingleTag,
                    a = t.selfUserId,
                    s = t.isDeleted,
                    l = void 0 !== s && s,
                    u = r === a;
                  return Object(f.jsxs)(et, {
                    children: [
                      Object(f.jsxs)(nt, {
                        children: [
                          Object(f.jsx)(it, { children: i.tag }),
                          void 0 !== i.userName &&
                            Object(f.jsx)(J, {
                              authorId: r,
                              tagUserId: i.userId,
                              selfUserId: a,
                              userName: i.userName,
                              isDeleted: l,
                            }),
                        ],
                      }),
                      !l &&
                        (u || !i.deletable || i.locked || o) &&
                        Object(f.jsx)($, {
                          disabled: n || !u || o || i.userId === r,
                          locked: i.locked || o,
                          onClick: this.handleLockClick,
                        }),
                      !l &&
                        !c &&
                        (u || (i.deletable && !o)) &&
                        Object(f.jsx)(K, {
                          disabled: n || (!u && (!i.deletable || o)),
                          onClick: this.handleDeleteClick,
                        }),
                      u &&
                        l &&
                        Object(f.jsx)(Q, {
                          disabled: n || (void 0 !== e && e >= 10),
                          onClick: this.handleRestoreClick,
                        }),
                    ],
                  });
                },
              },
            ]),
            n
          );
        })(O.a.Component),
        X = Object(m.connect)(function (t, e) {
          var n = U.c(t, H.q.Illust, e.id);
          return {
            selfUserId: Object(V.k)(t),
            busy: U.l(t),
            tagsLength: n ? n.tags.length : void 0,
          };
        })(Y);
      function $(t) {
        return Object(f.jsx)(at, {
          disabled: t.disabled,
          onClick: t.onClick,
          children: Object(f.jsx)(A, { size: 18, closed: t.locked }),
        });
      }
      function K(t) {
        return Object(f.jsx)(ct, {
          disabled: t.disabled,
          onClick: t.onClick,
          children: Object(f.jsx)(E.a, { size: 16 }),
        });
      }
      function Q(t) {
        return Object(f.jsx)(ct, {
          disabled: t.disabled,
          onClick: t.onClick,
          children: Object(f.jsx)(L.a, { size: 16 }),
        });
      }
      function J(t) {
        var e = t.authorId,
          n = t.tagUserId,
          i = t.selfUserId,
          r = t.userName,
          o = t.isDeleted,
          c = e === i,
          a = n === i,
          s = Object(w.p)(w.m.work.ns),
          l = Object(C.a)(s, 1)[0],
          u = Object(w.o)(),
          d = Object(C.a)(u, 1)[0];
        if (!c && !a) return null;
        if (a)
          return Object(f.jsx)(tt, {
            children: l(
              o
                ? w.m.work.info_by_user.deleted_tag
                : w.m.work.info_by_user.tag_added_by_you
            ),
          });
        var b = n ? W.user.member({}, { id: n }) : "";
        return Object(f.jsx)(tt, {
          children: d(o ? "%(user)�뺛굯�뚦뎷��" : "%(user)�뺛굯�뚩옙��", {
            user: n ? Object(f.jsx)(ot, { href: b, children: r }) : r,
          }),
        });
      }
      function tt(t) {
        return Object(f.jsx)(rt, { children: t.children });
      }
      var et = _.default.li.withConfig({ componentId: "sc-4mirup-0" })(
          [
            "display:flex;align-items:center;list-style:none;margin:0;padding:0 24px;min-height:64px;background:",
            ";&:hover{background:",
            ";}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.surface1;
          }.bind(void 0),
          function (t) {
            Object(i.a)(this, void 0);
            var e = t.theme;
            return Object(G.a)(e.materials.surface1, e.effects.hover);
          }.bind(void 0)
        ),
        nt = _.default.div.withConfig({ componentId: "sc-4mirup-1" })([
          "margin-right:auto;",
        ]),
        it = _.default.div.withConfig({ componentId: "sc-4mirup-2" })(
          [
            "color:",
            ";font-size:1rem;word-break:break-all;&::before{content:'#';}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.materials.text2;
          }.bind(void 0)
        ),
        rt = _.default.div.withConfig({ componentId: "sc-4mirup-3" })([
          "font-size:0.86rem;color:#999;margin-top:4px;",
        ]),
        ot = _.default.a.withConfig({ componentId: "sc-4mirup-4" })([
          "text-decoration:none;color:#3d7699;",
        ]),
        ct = _.default.button.withConfig({ componentId: "sc-4mirup-5" })([
          "box-sizing:border-box;width:24px;height:24px;border:none;background:none;margin:0 4px;padding:4px;line-height:0;&:not(:disabled){cursor:pointer;}&:last-child{margin-right:0;}&:disabled{opacity:0.4;}",
        ]),
        at = Object(_.default)(ct).withConfig({ componentId: "sc-4mirup-6" })([
          "padding:3px 4px;",
        ]),
        st = (n(31), n(292));
      function lt(t) {
        var e = t.title,
          n = t.limit,
          i = t.tags,
          r = t.children;
        return Object(f.jsx)(st.a, {
          title: e,
          auxLabel: n ? "".concat(i.length, "/").concat(n) : void 0,
          children: Object(f.jsx)(ut, { children: i.map(r) }),
        });
      }
      var ut = _.default.ul.withConfig({ componentId: "sc-1fryw2z-0" })([
        "margin:0 -24px;padding:0;",
      ]);
      function dt(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(b.a)(t);
          if (e) {
            var r = Object(b.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(d.a)(this, n);
        };
      }
      var bt = (function (t) {
          Object(u.a)(n, t);
          var e = dt(n);
          function n() {
            var t,
              r = this;
            Object(a.a)(this, n);
            for (var o = arguments.length, c = new Array(o), s = 0; s < o; s++)
              c[s] = arguments[s];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(h.a)(
                Object(l.a)(t),
                "renderTag",
                function (e) {
                  return (
                    Object(i.a)(this, r),
                    Object(f.jsx)(
                      X,
                      {
                        authorId: t.props.authorId,
                        isDeleted: !0,
                        isLocked: !1,
                        isSingleTag: !1,
                        workType: t.props.workType,
                        id: t.props.id,
                        onError: t.props.onError,
                        tag: e,
                      },
                      e.key
                    )
                  );
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(s.a)(n, [
              {
                key: "componentDidMount",
                value: function () {
                  this.props.dispatch(U.h(this.props.workType, this.props.id));
                },
              },
              {
                key: "componentDidUpdate",
                value: function (t) {
                  (this.props.workType === t.workType &&
                    this.props.id === t.id) ||
                    this.props.dispatch(
                      U.h(this.props.workType, this.props.id)
                    );
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this,
                    e = this.props.historyTags;
                  if (void 0 === e || 0 === e.length) return null;
                  var n = [],
                    r = e.filter(
                      function (e) {
                        var r = this;
                        Object(i.a)(this, t);
                        var o = e.tag,
                          c = e.userId;
                        return (
                          !n.some(
                            function (t) {
                              return (
                                Object(i.a)(this, r),
                                t.tag === o && t.userId === c
                              );
                            }.bind(this)
                          ) && (n.push({ tag: o, userId: c }), !0)
                        );
                      }.bind(this)
                    );
                  return Object(f.jsx)(w.f, {
                    ns: w.m.work.ns,
                    children: function (e) {
                      return (
                        Object(i.a)(this, t),
                        Object(f.jsx)(lt, {
                          title: e(w.m.work.info_by_user.deleted_tag),
                          tags: r,
                          limit: 0,
                          children: this.renderTag,
                        })
                      );
                    }.bind(this),
                  });
                },
              },
            ]),
            n
          );
        })(O.a.Component),
        ht = Object(m.connect)(function (t, e) {
          return { historyTags: U.d(t, e.workType, e.id) || void 0 };
        })(bt),
        ft = n(460),
        jt = n(19),
        pt = n(18),
        Ot = n(257),
        mt = n(1323),
        vt = n(9),
        gt = n(286),
        xt = n(105),
        yt = n(194),
        wt = n(1303),
        kt = n(1298);
      function Ct(t) {
        var e = this,
          n = t.show,
          r = t.onConfirm,
          o = t.onCancel,
          c = t.busy,
          a = Object(vt.a)(
            function () {
              Object(i.a)(this, e), r();
            }.bind(this),
            [r]
          ),
          s = Object(w.p)([w.m.linked_service.ns, w.m.func.ns]),
          l = Object(C.a)(s, 1)[0];
        return Object(f.jsxs)(xt.a, {
          show: n,
          onClose: o,
          modalWidth: 440,
          children: [
            Object(f.jsx)(kt.d, {
              children: l(w.m.linked_service.modal_on_toggle_settings),
            }),
            Object(f.jsxs)(kt.a, {
              children: [
                Object(f.jsx)(kt.b, {
                  children: l(
                    w.m.linked_service.modal_on_toggle_settings_works_hidden
                  ),
                }),
                Object(f.jsx)(wt.b, {
                  children: Object(f.jsx)(wt.a, {
                    children: l(
                      w.m.linked_service
                        .modal_on_toggle_settings_works_hidden_details
                    ),
                  }),
                }),
                Object(f.jsx)(kt.b, {
                  children: l(
                    w.m.linked_service.modal_on_toggle_settings_check_required
                  ),
                }),
                Object(f.jsx)(kt.b, {
                  children: l(
                    w.m.linked_service.modal_on_toggle_works_how_to_remove
                  ),
                }),
                Object(f.jsx)(wt.b, {
                  children: Object(f.jsxs)(w.e, {
                    i18nKey:
                      w.m.linked_service
                        .modal_on_toggle_works_how_to_remove_details,
                    ns: w.m.linked_service.ns,
                    children: [
                      Object(f.jsx)(wt.a, {}),
                      Object(f.jsx)(wt.a, {}),
                    ],
                  }),
                }),
              ],
            }),
            Object(f.jsxs)(yt.c, {
              children: [
                Object(f.jsx)(gt.a, {
                  variant: "Primary",
                  onClick: a,
                  disabled: c,
                  children: l(w.m.linked_service.unlink_work),
                }),
                Object(f.jsx)(gt.a, {
                  onClick: o,
                  children: l(w.m.func.setting_modal.cancel),
                }),
              ],
            }),
          ],
        });
      }
      function It(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function _t(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? It(Object(n), !0).forEach(function (e) {
                Object(h.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : It(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function St(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(b.a)(t);
          if (e) {
            var r = Object(b.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(d.a)(this, n);
        };
      }
      var Pt = (function (t) {
          Object(u.a)(r, t);
          var e,
            n = St(r);
          function r() {
            var t,
              e = this;
            Object(a.a)(this, r);
            for (var s = arguments.length, u = new Array(s), d = 0; d < s; d++)
              u[d] = arguments[d];
            return (
              (t = n.call.apply(n, [this].concat(u))),
              Object(h.a)(Object(l.a)(t), "state", {
                currentTime: void 0,
                error: !1,
                showLemonUnlinkModal: !1,
                isLemonMakePrivateConfirmNeeded: !1,
                addingTag: null,
              }),
              Object(h.a)(Object(l.a)(t), "transition", {
                unique: !0,
                from: { transform: "scaleY(0)" },
                enter: { transform: "scaleY(1)" },
                leave: { transform: "scaleY(0)" },
              }),
              Object(h.a)(Object(l.a)(t), "timer", void 0),
              Object(h.a)(Object(l.a)(t), "errorTimer", void 0),
              Object(h.a)(
                Object(l.a)(t),
                "handleLemonUnlinkConfirm",
                Object(c.a)(
                  o.a.mark(function e() {
                    return o.a.wrap(
                      function (e) {
                        for (;;)
                          switch ((e.prev = e.next)) {
                            case 0:
                              if (t.state.addingTag) {
                                e.next = 2;
                                break;
                              }
                              return e.abrupt("return");
                            case 2:
                              return (
                                t.setState({ showLemonUnlinkModal: !1 }),
                                (e.prev = 3),
                                (e.next = 6),
                                t.props.dispatch(
                                  U.b(
                                    t.props.workType,
                                    t.props.id,
                                    t.state.addingTag
                                  )
                                )
                              );
                            case 6:
                              e.next = 11;
                              break;
                            case 8:
                              (e.prev = 8),
                                (e.t0 = e.catch(3)),
                                t.handleError(e.t0);
                            case 11:
                            case "end":
                              return e.stop();
                          }
                      },
                      e,
                      null,
                      [[3, 8]]
                    );
                  })
                )
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleLemonUnlinkCancel",
                function () {
                  Object(i.a)(this, e),
                    t.setState({ showLemonUnlinkModal: !1 });
                }.bind(this)
              ),
              Object(h.a)(
                Object(l.a)(t),
                "renderTag",
                function (n) {
                  Object(i.a)(this, e);
                  var r = t.props,
                    o = r.tagData,
                    c = r.isEditable;
                  return void 0 === o
                    ? null
                    : Object(f.jsx)(
                        X,
                        {
                          authorId: o.authorId,
                          isLocked: o.isLocked || !c(t.state.currentTime),
                          isSingleTag: 0 === o.tags.length,
                          workType: t.props.workType,
                          id: t.props.id,
                          onError: t.handleError,
                          tag: n,
                        },
                        n.tag
                      );
                }.bind(this)
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleTagAdd",
                (function () {
                  var e = Object(c.a)(
                    o.a.mark(function e(n) {
                      var i;
                      return o.a.wrap(
                        function (e) {
                          for (;;)
                            switch ((e.prev = e.next)) {
                              case 0:
                                if (
                                  ((e.prev = 0),
                                  (i = n.trim().replace(/^#+/, "")),
                                  !t.state.isLemonMakePrivateConfirmNeeded ||
                                    !["R-18", "R-18G"].includes(i))
                                ) {
                                  e.next = 5;
                                  break;
                                }
                                return (
                                  t.setState({
                                    showLemonUnlinkModal: !0,
                                    addingTag: i,
                                  }),
                                  e.abrupt("return")
                                );
                              case 5:
                                return (
                                  (e.next = 7),
                                  t.props.dispatch(
                                    U.b(t.props.workType, t.props.id, i)
                                  )
                                );
                              case 7:
                                e.next = 12;
                                break;
                              case 9:
                                (e.prev = 9),
                                  (e.t0 = e.catch(0)),
                                  t.handleError(e.t0);
                              case 12:
                              case "end":
                                return e.stop();
                            }
                        },
                        e,
                        null,
                        [[0, 9]]
                      );
                    })
                  );
                  return function (t) {
                    return e.apply(this, arguments);
                  };
                })()
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleLockChange",
                (function () {
                  var e = Object(c.a)(
                    o.a.mark(function e(n) {
                      var i;
                      return o.a.wrap(
                        function (e) {
                          for (;;)
                            switch ((e.prev = e.next)) {
                              case 0:
                                return (
                                  (i = n.target.checked ? U.r : U.m),
                                  (e.prev = 1),
                                  (e.next = 4),
                                  t.props.dispatch(
                                    i(t.props.workType, t.props.id)
                                  )
                                );
                              case 4:
                                e.next = 9;
                                break;
                              case 6:
                                (e.prev = 6),
                                  (e.t0 = e.catch(1)),
                                  t.handleError(e.t0);
                              case 9:
                              case "end":
                                return e.stop();
                            }
                        },
                        e,
                        null,
                        [[1, 6]]
                      );
                    })
                  );
                  return function (t) {
                    return e.apply(this, arguments);
                  };
                })()
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleError",
                function (n) {
                  if ((Object(i.a)(this, e), n instanceof Ot.a))
                    t.setState({
                      error: (n.apiError && n.apiError.message) || !0,
                    });
                  else {
                    if (!(n instanceof U.a)) throw n;
                    t.setState({ error: !0 });
                  }
                }.bind(this)
              ),
              Object(h.a)(
                Object(l.a)(t),
                "handleInterval",
                function () {
                  Object(i.a)(this, e),
                    t.setState({ currentTime: Object(j.default)() });
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(s.a)(
              r,
              [
                {
                  key: "getLemonLinkedState",
                  value:
                    ((e = Object(c.a)(
                      o.a.mark(function t() {
                        var e, n;
                        return o.a.wrap(
                          function (t) {
                            for (;;)
                              switch ((t.prev = t.next)) {
                                case 0:
                                  if (
                                    this.props.workType !== H.q.Illust ||
                                    (null === (e = this.props.tagData) ||
                                    void 0 === e
                                      ? void 0
                                      : e.authorId) !== this.props.selfUserId
                                  ) {
                                    t.next = 7;
                                    break;
                                  }
                                  return (
                                    (t.next = 3),
                                    Object(mt.c)(
                                      this.props.apiClient,
                                      this.props.id
                                    )
                                  );
                                case 3:
                                  (n = t.sent),
                                    this.setState({
                                      isLemonMakePrivateConfirmNeeded:
                                        !!n.linkState &&
                                        jt.e.isLemonIllustMakePrivateConfirmNeeded(
                                          n.linkState
                                        ),
                                    }),
                                    (t.next = 8);
                                  break;
                                case 7:
                                  this.setState({
                                    isLemonMakePrivateConfirmNeeded: !1,
                                  });
                                case 8:
                                case "end":
                                  return t.stop();
                              }
                          },
                          t,
                          this
                        );
                      })
                    )),
                    function () {
                      return e.apply(this, arguments);
                    }),
                },
                {
                  key: "componentDidMount",
                  value: function () {
                    (this.timer = window.setInterval(this.handleInterval, 1e4)),
                      this.props.show &&
                        (this.props.dispatch(
                          U.g(this.props.workType, this.props.id, !0)
                        ),
                        this.getLemonLinkedState());
                  },
                },
                {
                  key: "componentDidUpdate",
                  value: function (t, e) {
                    var n = this,
                      r = this.props,
                      o = r.show,
                      c = r.id,
                      a = r.dispatch,
                      s = r.workType,
                      l = this.state,
                      u = l.currentTime,
                      d = l.error;
                    !o ||
                      (t.show && c === t.id) ||
                      (a(U.g(s, c, !0)), this.getLemonLinkedState()),
                      u === e.currentTime &&
                        this.setState({ currentTime: Object(j.default)() }),
                      d &&
                        !e.error &&
                        (void 0 !== this.errorTimer &&
                          window.clearTimeout(this.errorTimer),
                        (this.errorTimer = window.setTimeout(
                          function () {
                            Object(i.a)(this, n), this.setState({ error: !1 });
                          }.bind(this),
                          5e3
                        ))),
                      void 0 !== this.timer && window.clearInterval(this.timer),
                      (this.timer = window.setInterval(
                        this.handleInterval,
                        1e4
                      ));
                  },
                },
                {
                  key: "componentWillUnmount",
                  value: function () {
                    void 0 !== this.timer && window.clearInterval(this.timer),
                      void 0 !== this.errorTimer &&
                        window.clearTimeout(this.errorTimer);
                  },
                },
                {
                  key: "render",
                  value: function () {
                    var t = this,
                      e = this.props,
                      n = e.workType,
                      r = e.id,
                      o = e.show,
                      c = e.busy,
                      a = e.tagData,
                      s = e.selfUserId,
                      l = e.onClose,
                      u = e.isEditable,
                      d = e.isInCooldownPeriod,
                      b = this.state,
                      h = b.currentTime,
                      j = b.error,
                      p = b.showLemonUnlinkModal,
                      O = void 0 !== a && a.authorId === s,
                      m = u(h),
                      g = d(h),
                      C =
                        void 0 === a
                          ? Object(f.jsx)(ft.a, { loading: !0 })
                          : Object(f.jsx)(w.f, {
                              ns: [w.m.work.ns, w.m.error.ns],
                              children: function (e) {
                                var o = this;
                                return (
                                  Object(i.a)(this, t),
                                  Object(f.jsxs)(f.Fragment, {
                                    children: [
                                      Object(f.jsx)(
                                        v.a,
                                        _t(
                                          _t({}, this.transition),
                                          {},
                                          {
                                            items: g || !1 !== j,
                                            children: function (t, n, r) {
                                              var c = this;
                                              return (
                                                Object(i.a)(this, o),
                                                function (n) {
                                                  return (
                                                    Object(i.a)(this, c),
                                                    t &&
                                                      Object(f.jsx)(
                                                        Mt,
                                                        {
                                                          error: !1 !== j,
                                                          style: n,
                                                          children: j
                                                            ? "string" ==
                                                              typeof j
                                                              ? j
                                                              : e(
                                                                  w.m.error
                                                                    .general_error
                                                                )
                                                            : e(
                                                                w.m.work
                                                                  .info_by_user
                                                                  .tag_edit_completed
                                                              ),
                                                        },
                                                        r
                                                      )
                                                  );
                                                }.bind(this)
                                              );
                                            }.bind(this),
                                          }
                                        )
                                      ),
                                      !O &&
                                        !a.isLocked &&
                                        Object(f.jsx)(k.a, {
                                          showIcon: !0,
                                          title: e(
                                            w.m.work.info_by_user
                                              .tag_does_not_locked
                                          ),
                                          children: Object(f.jsx)(Nt, {
                                            children: e(
                                              w.m.work.info_by_user
                                                .tag_edit_attention
                                            ),
                                          }),
                                        }),
                                      Object(f.jsxs)(Tt, {
                                        children: [
                                          Object(f.jsxs)(Rt, {
                                            children: [
                                              Object(f.jsx)(lt, {
                                                title: e(
                                                  w.m.work.info_by_user.tag
                                                ),
                                                tags: a.tags,
                                                limit: 10,
                                                children: this.renderTag,
                                              }),
                                              O &&
                                                Object(f.jsx)(ht, {
                                                  workType: n,
                                                  id: r,
                                                  authorId: a.authorId,
                                                  onError: this.handleError,
                                                }),
                                            ],
                                          }),
                                          (O ||
                                            (!a.isLocked &&
                                              m &&
                                              a.tags.length < 10)) &&
                                            Object(f.jsx)(M, {
                                              disabled: c,
                                              onTagAdd: this.handleTagAdd,
                                            }),
                                        ],
                                      }),
                                      O &&
                                        Object(f.jsxs)(f.Fragment, {
                                          children: [
                                            Object(f.jsx)(zt, {}),
                                            Object(f.jsx)(S, {
                                              disabled: c,
                                              checked: !a.isLocked,
                                              onChange: this.handleLockChange,
                                            }),
                                          ],
                                        }),
                                    ],
                                  })
                                );
                              }.bind(this),
                            });
                    return Object(f.jsxs)(f.Fragment, {
                      children: [
                        Object(f.jsx)(w.f, {
                          ns: [w.m.work.ns, w.m.general.ns],
                          children: function (e) {
                            return (
                              Object(i.a)(this, t),
                              Object(f.jsxs)(x.e, {
                                show: o,
                                onBackgroundClick: l,
                                isForm: !1,
                                children: [
                                  Object(f.jsx)(x.d, {
                                    onCloseClick: l,
                                    children: e(
                                      w.m.work.info_by_user.edit_work_tag
                                    ),
                                  }),
                                  Object(f.jsx)(x.a, {
                                    children: Object(f.jsx)(Dt, {
                                      tagData: a,
                                      children: C,
                                    }),
                                  }),
                                  Object(f.jsx)(y.a, {
                                    okButton: !1,
                                    cancelButtonLabel: e(
                                      w.m.general.button.close_modal
                                    ),
                                    onCancelClick: l,
                                    stacked: !0,
                                  }),
                                ],
                              })
                            );
                          }.bind(this),
                        }),
                        Object(f.jsx)(Ct, {
                          show: p,
                          onCancel: this.handleLemonUnlinkCancel,
                          onConfirm: this.handleLemonUnlinkConfirm,
                        }),
                      ],
                    });
                  },
                },
              ],
              [
                {
                  key: "getDerivedStateFromProps",
                  value: function () {
                    return { currentTime: Object(j.default)() };
                  },
                },
              ]
            ),
            r
          );
        })(O.a.Component),
        Dt =
          ((e.a = Object(m.connect)(function (t, e) {
            var n = e.workType,
              i = e.id;
            return {
              tagData: U.c(t, n, i),
              selfUserId: V.k(t),
              busy: U.l(t),
              isEditable: U.o(t, n, i),
              isInCooldownPeriod: U.p(t, n, i),
              apiClient: Object(pt.d)(t),
            };
          })(Pt)),
          _.default.div.withConfig({ componentId: "j6xcve-0" })(
            ["width:392px;min-height:262px;", ""],
            function (t) {
              return (
                Object(i.a)(this, void 0),
                void 0 === t.tagData &&
                  Object(_.css)([
                    "&&{display:flex;justify-content:center;align-items:center;}",
                  ])
              );
            }.bind(void 0)
          )),
        Tt = _.default.div.withConfig({ componentId: "j6xcve-1" })([
          "display:flex;flex-flow:column;margin:0 -24px;",
        ]),
        Rt = _.default.div.withConfig({ componentId: "j6xcve-2" })([
          "flex:auto;overflow-y:auto;padding:0 24px;> div{margin-bottom:0;}",
        ]),
        zt = _.default.hr.withConfig({ componentId: "j6xcve-3" })([
          "border:none;border-top:1px solid #eee;margin:0 -24px;padding:0;",
        ]),
        Nt = _.default.p.withConfig({ componentId: "j6xcve-4" })([
          "max-width:300px;margin:0 0 0 20px;",
        ]),
        Mt = Object(_.default)(g.a.div).withConfig({ componentId: "j6xcve-5" })(
          [
            "position:relative;transform-origin:top;box-sizing:border-box;height:46px;margin:0 -24px -46px;border-bottom:1px solid #eee;z-index:1;background:#fffe;text-align:center;color:#b1cc29;font-size:14px;line-height:46px;",
            "",
          ],
          function (t) {
            Object(i.a)(this, void 0);
            var e = t.error,
              n = t.theme;
            return e && Object(_.css)(["&&{color:", ";}"], n.semantic.error);
          }.bind(void 0)
        );
    },
    1460: function (t, e, n) {
      "use strict";
      n(52), n(85), n(31), n(176);
      var i = n(4),
        r = n(1),
        o = n(8),
        c = n(0),
        a = n(61),
        s = n(9),
        l = n(496),
        u = n(460),
        d = (n(6), n(51)),
        b = n(301),
        h = n(663),
        f = n(688),
        j = n(360),
        p = n(361),
        O = n(130),
        m = n(66),
        v = n(494),
        g = n(547),
        x = n(1302),
        y = n(3),
        w = n(20),
        k = n(26),
        C = n(428),
        I = n(95),
        _ = n(2),
        S = n(5);
      function P(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function D(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? P(Object(n), !0).forEach(function (e) {
                Object(i.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : P(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function T(t, e) {
        var n;
        if ("undefined" == typeof Symbol || null == t[Symbol.iterator]) {
          if (
            Array.isArray(t) ||
            (n = (function (t, e) {
              if (!t) return;
              if ("string" == typeof t) return R(t, e);
              var n = Object.prototype.toString.call(t).slice(8, -1);
              "Object" === n && t.constructor && (n = t.constructor.name);
              if ("Map" === n || "Set" === n) return Array.from(t);
              if (
                "Arguments" === n ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)
              )
                return R(t, e);
            })(t)) ||
            (e && t && "number" == typeof t.length)
          ) {
            n && (t = n);
            var i = 0,
              r = function () {};
            return {
              s: r,
              n: function () {
                return i >= t.length
                  ? { done: !0 }
                  : { done: !1, value: t[i++] };
              },
              e: function (t) {
                throw t;
              },
              f: r,
            };
          }
          throw new TypeError(
            "Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
          );
        }
        var o,
          c = !0,
          a = !1;
        return {
          s: function () {
            n = t[Symbol.iterator]();
          },
          n: function () {
            var t = n.next();
            return (c = t.done), t;
          },
          e: function (t) {
            (a = !0), (o = t);
          },
          f: function () {
            try {
              c || null == n.return || n.return();
            } finally {
              if (a) throw o;
            }
          },
        };
      }
      function R(t, e) {
        (null == e || e > t.length) && (e = t.length);
        for (var n = 0, i = new Array(e); n < e; n++) i[n] = t[n];
        return i;
      }
      var z = { fetchWorks: C.d },
        N = Object(d.connect)(function (t, e) {
          var n = e.workType,
            i = e.id,
            r = Object(I.c)(t, n, i),
            o = Object(k.h)(t),
            c =
              void 0 === r
                ? void 0
                : o && Object(k.l)(t) !== w.k.Safe && r.xRestrict !== y.r.Safe
                ? w.a.All
                : w.a.Safe;
          return { works: C.b(t, n, i), mode: c };
        }, z)(function (t) {
          var e = this,
            n = t.workType,
            i = t.id,
            d = t.works,
            O = t.mode,
            m = t.fetchWorks,
            v = t.viewMore,
            x = t.viewMoreLimitNum,
            w = Object(b.d)(l.a.RelatedWorks),
            k = Object(I.o)(n, i),
            C = Object(S.p)([S.m.view.ns, S.m.action.ns]),
            _ = Object(o.a)(C, 1)[0],
            P = Object(s.i)(null),
            R = Object(s.j)(!v),
            z = Object(o.a)(R, 2),
            N = z[0],
            E = z[1],
            L = Object(s.a)(
              function () {
                Object(r.a)(this, e), E(!0);
              }.bind(this),
              []
            ),
            A = null === d;
          Object(s.d)(
            function () {
              var t = this;
              Object(r.a)(this, e);
              var o = P.current;
              if (null !== o && void 0 !== O) {
                var c = new IntersectionObserver(
                  function (e) {
                    Object(r.a)(this, t);
                    var o,
                      c = !1,
                      a = T(e);
                    try {
                      for (a.s(); !(o = a.n()).done; ) {
                        var s = o.value;
                        c = s.isIntersecting || s.intersectionRatio > 0;
                      }
                    } catch (l) {
                      a.e(l);
                    } finally {
                      a.f();
                    }
                    c && null === d && m(n, i, O);
                  }.bind(this),
                  { rootMargin: "300px" }
                );
                return (
                  c.observe(o),
                  function () {
                    Object(r.a)(this, t), c.disconnect();
                  }.bind(this)
                );
              }
            }.bind(this),
            [m, i, n, d, O, v, x]
          );
          var F = (null != d ? d : [])
              .filter(
                function (t) {
                  return (
                    Object(r.a)(this, e),
                    !t.isAdContainer ||
                      ((null == k ? void 0 : k.xRestrict) === y.r.Safe && !w)
                  );
                }.bind(this)
              )
              .map(
                function (t, n) {
                  return (
                    Object(r.a)(this, e),
                    t.isAdContainer
                      ? { id: "ad_".concat(n), size: 1, mix: "ad" }
                      : D(D({}, t), {}, { mix: void 0 })
                  );
                }.bind(this)
              ),
            B = Math.ceil(F.length / 5);
          return Object(c.jsxs)(c.Fragment, {
            children: [
              null !== d &&
                d.length > 0 &&
                Object(c.jsxs)(j.a, {
                  children: [
                    Object(c.jsx)(p.c, {
                      children: _(S.m.view.discovery.recommended_work),
                    }),
                    n === y.q.Illust
                      ? Object(c.jsx)(h.a, { row: B, items: F, factory: M })
                      : n === y.q.Novel
                      ? Object(c.jsx)(f.a, { row: B, items: F, factory: M })
                      : a.b.unreachable(),
                  ],
                }),
              Object(c.jsx)("div", {
                ref: P,
                children:
                  A &&
                  Object(c.jsx)(q, {
                    margin: v ? 100 : void 0,
                    children: Object(c.jsx)(u.a, { loading: !0, size: 32 }),
                  }),
              }),
              v &&
                !N &&
                Object(c.jsx)(U, {
                  children: Object(c.jsx)(g.a, {
                    onClick: L,
                    children: _(S.m.action.link.see_more_comment),
                  }),
                }),
            ],
          });
        });
      function M(t, e) {
        return "ad" === t.mix
          ? Object(c.jsx)(b.c, { kind: l.a.RelatedWorks })
          : t.type === y.q.Illust
          ? Object(c.jsx)(O.d, {
              size: m.c.Size184,
              type: y.q.Illust,
              thumbnail: t,
              position: e,
            })
          : t.type === y.q.Novel
          ? Object(c.jsx)("div", {
              className: "gtm-novel-work-recommend-link",
              "data-recommend-method": "user2work",
              "data-recommend-work-id": t.id,
              children: Object(c.jsx)(v.b, {
                size: v.a.SizeAuto,
                type: y.q.Novel,
                thumbnail: t,
              }),
            })
          : a.b.unreachable(t);
      }
      var q = _.default.div.withConfig({ componentId: "sc-1gt2y5m-0" })(
          ["margin:96px 0 ", ";"],
          function (t) {
            return (
              Object(r.a)(this, void 0),
              void 0 !== t.margin ? "".concat(t.margin, "px") : "100vh"
            );
          }.bind(void 0)
        ),
        U = _.default.div.withConfig({ componentId: "sc-1gt2y5m-1" })([
          "width:600px;margin:40px auto 0;",
        ]),
        E = n(382);
      function L(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function A(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? L(Object(n), !0).forEach(function (e) {
                Object(i.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : L(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function F(t, e) {
        var n;
        if ("undefined" == typeof Symbol || null == t[Symbol.iterator]) {
          if (
            Array.isArray(t) ||
            (n = (function (t, e) {
              if (!t) return;
              if ("string" == typeof t) return B(t, e);
              var n = Object.prototype.toString.call(t).slice(8, -1);
              "Object" === n && t.constructor && (n = t.constructor.name);
              if ("Map" === n || "Set" === n) return Array.from(t);
              if (
                "Arguments" === n ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)
              )
                return B(t, e);
            })(t)) ||
            (e && t && "number" == typeof t.length)
          ) {
            n && (t = n);
            var i = 0,
              r = function () {};
            return {
              s: r,
              n: function () {
                return i >= t.length
                  ? { done: !0 }
                  : { done: !1, value: t[i++] };
              },
              e: function (t) {
                throw t;
              },
              f: r,
            };
          }
          throw new TypeError(
            "Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
          );
        }
        var o,
          c = !0,
          a = !1;
        return {
          s: function () {
            n = t[Symbol.iterator]();
          },
          n: function () {
            var t = n.next();
            return (c = t.done), t;
          },
          e: function (t) {
            (a = !0), (o = t);
          },
          f: function () {
            try {
              c || null == n.return || n.return();
            } finally {
              if (a) throw o;
            }
          },
        };
      }
      function B(t, e) {
        (null == e || e > t.length) && (e = t.length);
        for (var n = 0, i = new Array(e); n < e; n++) i[n] = t[n];
        return i;
      }
      var W = { fetchWorkRecommends: E.g, fetchMoreRecommends: E.f };
      e.a = Object(d.connect)(function (t, e) {
        var n = e.workType,
          i = e.id;
        return {
          works: E.d(t, n, i),
          hasNext: E.b(t, n, i),
          nextIds: E.c(t, n, i),
          isBusy: E.i(t),
        };
      }, W)(function (t) {
        var e = this,
          n = t.workType,
          i = t.id,
          d = t.viewMore,
          O = void 0 !== d && d,
          m = t.works,
          v = t.isBusy,
          w = t.hasNext,
          k = t.nextIds,
          C = t.fetchWorkRecommends,
          _ = t.fetchMoreRecommends,
          P = Object(I.o)(n, i),
          D = Object(b.d)(l.a.RelatedWorks),
          T = Object(S.p)([S.m.action.ns, S.m.work.ns]),
          R = Object(o.a)(T, 1)[0],
          z = Object(s.i)(null),
          M = Object(s.j)(!O),
          q = Object(o.a)(M, 2),
          U = q[0],
          L = q[1],
          B = E.j(n, i),
          W = Object(s.a)(
            function () {
              Object(r.a)(this, e), L(!0);
            }.bind(this),
            []
          ),
          Y = O && null === m;
        Object(s.d)(
          function () {
            Object(r.a)(this, e), L(!O);
          }.bind(this),
          [i, O]
        );
        var X = n === y.q.Illust ? 30 : 6;
        if (
          (Object(s.d)(
            function () {
              Object(r.a)(this, e), Y && C(n, i, X);
            }.bind(this),
            [C, i, Y, v, O, X, n]
          ),
          Object(s.d)(
            function () {
              var t = this;
              Object(r.a)(this, e);
              var o = z.current;
              if (null !== o && w && !v && U) {
                var c = new IntersectionObserver(
                  function (e) {
                    Object(r.a)(this, t);
                    var o,
                      c = !1,
                      a = F(e);
                    try {
                      for (a.s(); !(o = a.n()).done; ) {
                        var s = o.value;
                        c = s.isIntersecting || s.intersectionRatio > 0;
                      }
                    } catch (l) {
                      a.e(l);
                    } finally {
                      a.f();
                    }
                    c && (k.length > 0 ? _(n, i) : C(n, i));
                  }.bind(this),
                  { rootMargin: "300px" }
                );
                return (
                  c.observe(o),
                  function () {
                    Object(r.a)(this, t), c.disconnect();
                  }.bind(this)
                );
              }
            }.bind(this),
            [_, C, w, i, U, v, k.length, n]
          ),
          null !== m && 0 === m.length && (n === y.q.Illust || n === y.q.Novel))
        )
          return Object(c.jsx)(N, {
            workType: n,
            id: i,
            viewMore: O && n === y.q.Novel,
            viewMoreLimitNum: X,
          });
        var $ = (null != m ? m : [])
            .filter(
              function (t) {
                return (
                  Object(r.a)(this, e),
                  !t.isAdContainer ||
                    ((null == P ? void 0 : P.xRestrict) === y.r.Safe && !D)
                );
              }.bind(this)
            )
            .map(
              function (t, n) {
                if ((Object(r.a)(this, e), t.isAdContainer))
                  return { id: "ad_".concat(n), size: 1, mix: "ad" };
                var i = null == B ? void 0 : B[t.id],
                  o = {
                    zone: y.c.NewIllustDetail,
                    recommendScore: null == i ? void 0 : i.score,
                    recommendMethod: null == i ? void 0 : i.methods.join(","),
                    recommendSeedIllustIds:
                      null == i ? void 0 : i.seedIllustIds.join(","),
                    recommendBanditInfo: null == i ? void 0 : i.banditInfo,
                    recommendListId: null == i ? void 0 : i.recommendListId,
                  };
                return A(A({}, t), {}, { gtmRecommendData: o, mix: void 0 });
              }.bind(this)
            ),
          K = Math.ceil($.length / 5);
        return Object(c.jsxs)(V, {
          children: [
            Object(c.jsxs)(j.a, {
              children: [
                Object(c.jsx)(p.c, {
                  as: "h2",
                  children: R(S.m.work.info_by_pixiv.related_work),
                }),
                Object(c.jsx)(x.a, {
                  gtmData: {
                    className: "illust-recommend",
                    recommendZone: y.c.NewIllustDetail,
                  },
                  disabled: n === y.q.Novel,
                  children:
                    n === y.q.Illust
                      ? Object(c.jsx)(h.a, { row: K, items: $, factory: H })
                      : n === y.q.Novel
                      ? Object(c.jsx)(f.a, { row: K, items: $, factory: H })
                      : a.b.unreachable(),
                }),
              ],
            }),
            Object(c.jsx)("div", {
              ref: z,
              children:
                w &&
                U &&
                Object(c.jsx)(Z, {
                  margin: O ? 100 : void 0,
                  children: Object(c.jsx)(u.a, { loading: !0, size: 32 }),
                }),
            }),
            O &&
              !U &&
              Object(c.jsx)(G, {
                children: Object(c.jsx)(g.a, {
                  onClick: W,
                  children: R(S.m.action.link.see_more_comment),
                }),
              }),
          ],
        });
      });
      function H(t, e) {
        return "ad" === t.mix
          ? Object(c.jsx)(b.c, { kind: l.a.RelatedWorks })
          : t.type === y.q.Illust
          ? Object(c.jsx)(O.d, {
              size: m.c.Size184,
              type: y.q.Illust,
              thumbnail: t,
              position: e,
              gtmRecommendData: t.gtmRecommendData,
            })
          : t.type === y.q.Novel
          ? Object(c.jsx)("div", {
              className: "gtm-novel-work-recommend-link",
              "data-recommend-method": "work2work",
              "data-recommend-work-id": t.id,
              children: Object(c.jsx)(v.b, {
                size: v.a.SizeAuto,
                type: y.q.Novel,
                thumbnail: t,
              }),
            })
          : a.b.unreachable(t);
      }
      var V = _.default.div.withConfig({ componentId: "sc-1d7d5ry-0" })([
          "overflow-anchor:none;",
        ]),
        G = _.default.div.withConfig({ componentId: "sc-1d7d5ry-1" })([
          "width:600px;margin:40px auto 0;",
        ]),
        Z = _.default.div.withConfig({ componentId: "sc-1d7d5ry-2" })(
          ["margin:96px 0 ", ";"],
          function (t) {
            return (
              Object(r.a)(this, void 0),
              void 0 !== t.margin ? "".concat(t.margin, "px") : "100vh"
            );
          }.bind(void 0)
        );
    },
    1463: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return q;
      });
      n(28);
      var i = n(1),
        r = n(34),
        o = n(37),
        c = n(24),
        a = n(36),
        s = n(45),
        l = n(30),
        u = n(4),
        d = n(0),
        b = n(6),
        h = n.n(b),
        f = n(5),
        j = n(219),
        p = (n(31), n(8)),
        O = n(114),
        m = n(14),
        v = n(87),
        g = n(3),
        x = n(11),
        y = n(175),
        w = n(2);
      function k(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function C(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? k(Object(n), !0).forEach(function (e) {
                Object(u.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : k(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function I(t) {
        var e = this,
          n = t.show,
          r = t.data,
          o = t.onClose,
          c = Object(f.p)(f.m.work.ns),
          a = Object(p.a)(c, 1)[0];
        return Object(d.jsxs)(v.e, {
          show: n,
          onBackgroundClick: o,
          children: [
            Object(d.jsx)(v.d, {
              onCloseClick: o,
              children: a(f.m.work.info_by_user.image_response_base_work),
            }),
            Object(d.jsx)(v.a, {
              children: Object(d.jsx)(P, {
                children: r.map(
                  function (t) {
                    return (
                      Object(i.a)(this, e),
                      Object(d.jsx)(
                        D,
                        {
                          children: Object(d.jsx)(T, {
                            to:
                              t.type === g.q.Illust
                                ? m.memberIllust.illust(
                                    {},
                                    { mode: "medium", illust_id: t.workId }
                                  )
                                : t.type === g.q.Novel
                                ? m.novel.show({}, { id: t.workId })
                                : Object(x.v)(t.type),
                            title: t.title,
                            children: Object(d.jsx)(_, C({}, t)),
                          }),
                        },
                        t.workId
                      )
                    );
                  }.bind(this)
                ),
              }),
            }),
          ],
        });
      }
      function _(t) {
        var e = t.imageUrl,
          n = t.title,
          i = t.userName;
        return Object(d.jsxs)(d.Fragment, {
          children: [
            Object(d.jsx)(R, { src: e }),
            Object(d.jsxs)("div", {
              children: [
                Object(d.jsx)(z, { children: n }),
                Object(d.jsx)(N, { children: i }),
              ],
            }),
          ],
        });
      }
      var S = h.a.memo(I),
        P = w.default.ul.withConfig({ componentId: "sc-17sjq2i-0" })([
          "list-style:none;margin:0 -24px;padding:0 0 24px;",
        ]),
        D = w.default.li.withConfig({ componentId: "sc-17sjq2i-1" })(
          [
            "background-color:",
            ";transition:0.2s background-color;&:hover{background-color:",
            ";}",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.background.clear;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.background.clearHover;
          }.bind(void 0)
        ),
        T = Object(w.default)(O.a).withConfig({ componentId: "sc-17sjq2i-2" })([
          "display:flex;align-items:center;padding:8px 24px;text-decoration:none;",
        ]),
        R = Object(w.default)(Object(y.e)("div", ["src"])).withConfig({
          componentId: "sc-17sjq2i-3",
        })(
          [
            "flex:none;margin-right:8px;width:48px;height:48px;border-radius:4px;background-repeat:no-repeat;background-size:contain;background-position:center;background-image:",
            ";",
          ],
          function (t) {
            Object(i.a)(this, void 0);
            var e = t.src;
            return Object(x.d)(e);
          }.bind(void 0)
        ),
        z = w.default.p.withConfig({ componentId: "sc-17sjq2i-4" })(
          [
            "margin:0 0 4px;padding:0;width:286px;text-overflow:ellipsis;white-space:nowrap;overflow:hidden;font-size:14px;font-weight:bold;color:",
            ";",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.default;
          }.bind(void 0)
        ),
        N = w.default.p.withConfig({ componentId: "sc-17sjq2i-5" })(
          ["margin:0;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.mediumGray;
          }.bind(void 0)
        );
      function M(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(l.a)(t);
          if (e) {
            var r = Object(l.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(s.a)(this, n);
        };
      }
      var q = (function (t) {
          Object(a.a)(n, t);
          var e = M(n);
          function n() {
            var t,
              o = this;
            Object(r.a)(this, n);
            for (var a = arguments.length, s = new Array(a), l = 0; l < a; l++)
              s[l] = arguments[l];
            return (
              (t = e.call.apply(e, [this].concat(s))),
              Object(u.a)(Object(c.a)(t), "state", { showModal: !1 }),
              Object(u.a)(
                Object(c.a)(t),
                "handleClick",
                function () {
                  Object(i.a)(this, o), t.setState({ showModal: !0 });
                }.bind(this)
              ),
              Object(u.a)(
                Object(c.a)(t),
                "handleClose",
                function () {
                  Object(i.a)(this, o), t.setState({ showModal: !1 });
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(o.a)(n, [
              {
                key: "componentDidUpdate",
                value: function (t, e) {
                  var n = this.props.imageResponseOutData;
                  this.state.showModal &&
                    e.showModal &&
                    n !== t.imageResponseOutData &&
                    this.setState({ showModal: !1 });
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this,
                    e = this.props.imageResponseOutData,
                    n = this.state.showModal;
                  return null !== e && e.length > 0
                    ? Object(d.jsxs)(d.Fragment, {
                        children: [
                          Object(d.jsxs)(U, {
                            onClick: this.handleClick,
                            children: [
                              Object(d.jsx)(E, {
                                children: Object(d.jsx)(f.f, {
                                  ns: f.m.work.ns,
                                  children: function (e) {
                                    return (
                                      Object(i.a)(this, t),
                                      e(
                                        f.m.work.info_by_user
                                          .image_response_base_work
                                      )
                                    );
                                  }.bind(this),
                                }),
                              }),
                              Object(d.jsx)(j.c, { value: e.length }),
                            ],
                          }),
                          Object(d.jsx)(S, {
                            data: e,
                            show: n,
                            onClose: this.handleClose,
                          }),
                        ],
                      })
                    : null;
                },
              },
            ]),
            n
          );
        })(h.a.Component),
        U = w.default.div.withConfig({ componentId: "e28d16-0" })(
          [
            "display:flex;justify-content:space-between;align-items:center;box-sizing:border-box;margin:24px 0;padding:0 16px;width:100%;height:46px;border-radius:4px;border:1px solid ",
            ";background-color:",
            ";cursor:pointer;",
          ],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.ui.border.default;
          }.bind(void 0),
          function (t) {
            return Object(i.a)(this, void 0), t.theme.background.clear;
          }.bind(void 0)
        ),
        E = w.default.span.withConfig({ componentId: "e28d16-1" })(
          ["font-weight:bold;line-height:1;color:", ";"],
          function (t) {
            return Object(i.a)(this, void 0), t.theme.text.default;
          }.bind(void 0)
        );
    },
    1497: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return x;
      });
      n(29), n(70), n(873), n(520);
      var i = n(7),
        r = n.n(i),
        o = (n(39), n(12)),
        c = n(1),
        a = n(8),
        s = n(0),
        l = n(246),
        u = n(19),
        d = n(9),
        b = (n(6), n(286)),
        h = n(719),
        f = n(15),
        j = n(71),
        p = n(2),
        O = n(5),
        m = (n(519), n(61)),
        v = n(412);
      function g(t) {
        var e = this,
          n = t.tooltipType,
          i = t.anchor,
          r = Object(O.p)([O.m.action_pixiv.ns, O.m.following.ns]),
          o = Object(a.a)(r, 1)[0],
          l = Object(d.g)(
            function () {
              return (
                Object(c.a)(this, e),
                n === u.k.MangaSeriesWatchListTooltipTypes.ShowWatchButton
                  ? o(O.m.following.watchlist.tooltip.show_watch_button)
                  : n === u.k.MangaSeriesWatchListTooltipTypes.AfterWatchSeries
                  ? o(O.m.following.watchlist.tooltip.after_watched)
                  : n ===
                    u.k.MangaSeriesWatchListTooltipTypes.ShowDashboardWatchCount
                  ? o(
                      O.m.following.watchlist.tooltip.show_dashboard_watch_count
                    )
                  : m.b.unreachable(n)
              );
            }.bind(this),
            [o, n]
          ),
          b = Object(d.g)(
            function () {
              return (
                Object(c.a)(this, e),
                n === u.k.MangaSeriesWatchListTooltipTypes.ShowWatchButton
                  ? {
                      children: o(O.m.action_pixiv.view_detail),
                      to: "https://www.pixiv.help/hc/articles/900004833123",
                      target: "_blank",
                      rel: "noopener noreferrer",
                    }
                  : n ===
                      u.k.MangaSeriesWatchListTooltipTypes.AfterWatchSeries ||
                    n ===
                      u.k.MangaSeriesWatchListTooltipTypes
                        .ShowDashboardWatchCount
                  ? void 0
                  : m.b.unreachable(n)
              );
            }.bind(this),
            [o, n]
          ),
          h = Object(d.j)(u.c.seriesWatchListTooltipStorage.isViewed(n)),
          f = Object(a.a)(h, 2),
          j = f[0],
          p = f[1],
          g = Object(d.a)(
            function () {
              Object(c.a)(this, e),
                u.c.seriesWatchListTooltipStorage.saveToViewed(n),
                p(!0);
            }.bind(this),
            [n]
          );
        return (
          Object(d.d)(
            function () {
              Object(c.a)(this, e),
                p(u.c.seriesWatchListTooltipStorage.isViewed(n)),
                n === u.k.MangaSeriesWatchListTooltipTypes.AfterWatchSeries &&
                  u.c.seriesWatchListTooltipStorage.saveToViewed(
                    u.k.MangaSeriesWatchListTooltipTypes.ShowWatchButton
                  );
            }.bind(this),
            [n]
          ),
          Object(s.jsx)(v.b, {
            anchor: i,
            show: !j,
            onClose: g,
            defaultTooltipConfig: {
              direction: "down",
              position: { align: "center", offset: 0 },
            },
            button: b,
            children: l,
          })
        );
      }
      function x(t) {
        var e = this,
          n = t.kind,
          i = t.fixed,
          p = void 0 !== i && i,
          m = t.seriesId,
          v = Object(O.p)([O.m.following.ns, O.m.error.ns]),
          x = Object(a.a)(v, 1)[0],
          w = Object(f.b)(),
          k = Object(d.j)(!1),
          C = Object(a.a)(k, 2),
          I = C[0],
          _ = C[1],
          S = Object(d.j)(),
          P = Object(a.a)(S, 2),
          D = P[0],
          T = P[1],
          R = Object(d.j)(!1),
          z = Object(a.a)(R, 2),
          N = z[0],
          M = z[1],
          q = Object(f.c)(
            function (t) {
              return Object(c.a)(this, e), t.work.series.watch.manga[m];
            }.bind(this)
          ),
          U = Object(d.i)(null),
          E = Object(d.j)(u.k.MangaSeriesWatchListTooltipTypes.ShowWatchButton),
          L = Object(a.a)(E, 2),
          A = L[0],
          F = L[1],
          B = Object(d.a)(
            Object(o.a)(
              r.a.mark(function t() {
                var e = this;
                return r.a.wrap(
                  function (t) {
                    for (;;)
                      switch ((t.prev = t.next)) {
                        case 0:
                          return (
                            M(!0),
                            F(
                              u.k.MangaSeriesWatchListTooltipTypes
                                .AfterWatchSeries
                            ),
                            (t.next = 4),
                            w(Object(j.c)(n, m))
                              .catch(
                                function (t) {
                                  if (
                                    (Object(c.a)(this, e),
                                    t instanceof l.b ? T(t.message) : T(void 0),
                                    _(!0),
                                    !(t instanceof l.b))
                                  )
                                    throw t;
                                }.bind(this)
                              )
                              .finally(
                                function () {
                                  Object(c.a)(this, e), M(!1);
                                }.bind(this)
                              )
                          );
                        case 4:
                        case "end":
                          return t.stop();
                      }
                  },
                  t,
                  this
                );
              })
            ),
            [w, n, m]
          ),
          W = Object(d.a)(
            Object(o.a)(
              r.a.mark(function t() {
                var e = this;
                return r.a.wrap(
                  function (t) {
                    for (;;)
                      switch ((t.prev = t.next)) {
                        case 0:
                          return (
                            M(!0),
                            (t.next = 3),
                            w(Object(j.U)(n, m))
                              .catch(
                                function (t) {
                                  throw (
                                    (Object(c.a)(this, e), T(void 0), _(!0), t)
                                  );
                                }.bind(this)
                              )
                              .finally(
                                function () {
                                  Object(c.a)(this, e), M(!1);
                                }.bind(this)
                              )
                          );
                        case 3:
                        case "end":
                          return t.stop();
                      }
                  },
                  t,
                  this
                );
              })
            ),
            [w, n, m]
          ),
          H = Object(d.a)(
            function () {
              Object(c.a)(this, e), _(!1);
            }.bind(this),
            []
          );
        return Object(s.jsxs)("div", {
          className: q ? "gtm-series-remove-button" : "gtm-seires-add-button",
          children: [
            Object(s.jsx)(b.a, {
              fixed: p,
              disabled: N,
              variant: q ? void 0 : "Primary",
              onClick: q ? W : B,
              children: Object(s.jsx)(y, {
                ref: U,
                children: x(
                  q
                    ? O.m.following.watchlist.remove_from_watchlist
                    : O.m.following.watchlist.add_to_watchlist
                ),
              }),
            }),
            Object(s.jsx)(g, { tooltipType: A, anchor: U }),
            Object(s.jsx)(h.a, {
              show: I,
              mainMessage: D || x(O.m.error.please_try_after_a_while),
              onClose: H,
            }),
          ],
        });
      }
      var y = p.default.div.withConfig({ componentId: "rsfcv7-0" })([
        "padding:9px 0;",
      ]);
    },
    1641: function (t, e, n) {
      "use strict";
      n.r(e),
        n.d(e, "default", function () {
          return ti;
        });
      var i = n(0),
        r = n(6),
        o = n.n(r),
        c = n(301),
        a = n(75),
        s = n(15),
        l = n(3),
        u = n(26),
        d = n(1370),
        b = n(1460),
        h = (n(302), n(31), n(362), n(8)),
        f = n(1),
        j = n(44),
        p = n(171),
        O = n(360),
        m = n(361),
        v = n(130),
        g = n(66),
        x = n(456),
        y = n(2),
        w = n(5),
        k = n(11),
        C = n(9),
        I = [];
      function _(t) {
        var e = this,
          n = t.workType,
          o = t.id,
          c = Object(s.c)(
            function (t) {
              return Object(f.a)(this, e), Object(x.a)(t, l.q.Illust, o);
            }.bind(this)
          ),
          d = Object(k.l)(c) ? c.zengoIdWorks : I,
          b = Object(s.c)(
            function (t) {
              return Object(f.a)(this, e), Object(u.r)(t);
            }.bind(this)
          ),
          y = Object(w.p)(w.m.work.ns),
          _ = Object(h.a)(y, 2),
          T = _[0],
          R = _[1].language,
          z = Object(r.useRef)(null),
          N = Object(r.useRef)(null),
          M = Object(j.g)(z),
          q = !b && Object(w.l)(R) && "en" === R,
          U = Object(C.g)(
            function () {
              var t = this;
              return (
                Object(f.a)(this, e),
                d
                  .map(
                    function (e, n) {
                      return (
                        Object(f.a)(this, t),
                        Object(i.jsx)(
                          v.d,
                          {
                            type: l.q.Illust,
                            currentId: o,
                            size: g.c.Size184,
                            thumbnail: d[n],
                            nofollow: d[n].id !== o && q,
                          },
                          n
                        )
                      );
                    }.bind(this)
                  )
                  .reverse()
              );
            }.bind(this),
            [d, o, q]
          );
        if (0 === d.length) return null;
        var E = Math.max(
            d.findIndex(
              function (t) {
                return Object(f.a)(this, e), t.id === o;
              }.bind(this)
            ),
            0
          ),
          L = void 0 !== M ? M.height : 0;
        return Object(i.jsxs)(O.a, {
          ref: N,
          children: [
            Object(i.jsx)(m.c, {
              as: "h2",
              children: T(w.m.work.everyones_work),
            }),
            Object(i.jsx)(D, { adapterSize: L }),
            Object(i.jsx)(P, {
              ref: z,
              yOffset: L,
              children: Object(i.jsx)(p.a, {
                buttonPadding: 16,
                bottomOffset: L - g.c.Size184,
                defaultScroll: {
                  align: "center",
                  offset:
                    (E + 1 - Math.ceil(d.length / 2)) *
                    (n === l.q.Illust ? 208 : 612),
                },
                children: Object(i.jsx)(a.b, {
                  children: Object(i.jsx)(S, { children: U }),
                }),
              }),
            }),
          ],
        });
      }
      var S = y.default.div.withConfig({ componentId: "sc-18n33ik-0" })([
          "display:flex;& > * + *{margin-left:24px;}",
        ]),
        P = y.default.div.withConfig({ componentId: "sc-18n33ik-1" })(
          [
            "position:absolute;width:100%;left:0;transform:translateY(-",
            "px);",
          ],
          function (t) {
            return Object(f.a)(this, void 0), t.yOffset;
          }.bind(void 0)
        ),
        D = y.default.div.withConfig({ componentId: "sc-18n33ik-2" })(
          ["height:", "px;"],
          function (t) {
            return Object(f.a)(this, void 0), t.adapterSize;
          }.bind(void 0)
        ),
        T = n(95),
        R = (n(28), n(65), n(34)),
        z = n(37),
        N = n(24),
        M = n(36),
        q = n(45),
        U = n(30),
        E = n(4),
        L = n(18),
        A = n(51),
        F = n(870),
        B = n(1347),
        W = n(1326),
        H = n(1371),
        V = n(1322),
        G = n(322),
        Z = n(877);
      function Y(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(U.a)(t);
          if (e) {
            var r = Object(U.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(q.a)(this, n);
        };
      }
      var X = { getWorkData: T.g, getUserData: u.m, overrideFollow: u.t },
        $ = (function (t) {
          Object(M.a)(n, t);
          var e = Y(n);
          function n() {
            var t;
            Object(R.a)(this, n);
            for (var i = arguments.length, r = new Array(i), o = 0; o < i; o++)
              r[o] = arguments[o];
            return (
              (t = e.call.apply(e, [this].concat(r))),
              Object(E.a)(Object(N.a)(t), "state", {}),
              t
            );
          }
          return (
            Object(z.a)(n, [
              {
                key: "componentDidMount",
                value: function () {
                  void 0 === this.props.illustData
                    ? this.props.getWorkData(l.q.Illust, this.props.idOrSecret)
                    : this.fetchUserData(this.props.illustData.userId);
                },
              },
              {
                key: "componentDidUpdate",
                value: function (t) {
                  void 0 === this.props.illustData ||
                    (void 0 !== t.illustData &&
                      this.props.illustData.userId === t.illustData.userId) ||
                    this.fetchUserData(this.props.illustData.userId),
                    Object(s.a)(t.idOrSecret, this.props.idOrSecret) ||
                      this.props.getWorkData(l.q.Illust, this.props.idOrSecret);
                  var e = this.props.userData;
                  void 0 !== e &&
                    e !== this.state.previousUserData &&
                    this.setState({ previousUserData: e });
                },
              },
              {
                key: "render",
                value: function () {
                  var t = this.props,
                    e = t.userData,
                    n = void 0 === e ? this.state.previousUserData : e,
                    r = t.comicPromotion,
                    o = t.fanboxPromotion,
                    a = t.currentWorkId,
                    s = t.isLoggedIn;
                  return Object(i.jsxs)(i.Fragment, {
                    children: [
                      n
                        ? Object(i.jsx)(B.a, {
                            userId: n.id,
                            pageType: l.j.Illust,
                            currentWorkId: a,
                            name: n.name,
                            imageUrl: n.profileImg,
                            isUnlisted: Object(T.i)(this.props.idOrSecret),
                          })
                        : Object(i.jsx)(Z.a, {}),
                      r && Object(i.jsx)(H.a, { comicPromotion: r }),
                      o &&
                        Object(i.jsx)(V.a, {
                          fanboxPromotion: o,
                          isLoggedIn: s,
                        }),
                      n &&
                        s &&
                        Object(i.jsx)(W.a, { userId: n.id, userName: n.name }),
                      Object(i.jsx)(Q, {
                        children: Object(i.jsx)(c.c, { kind: c.a.Rectangle }),
                      }),
                      Object(i.jsx)(F.a, {
                        children: Object(i.jsx)(c.c, { kind: c.a.Responsive }),
                      }),
                    ],
                  });
                },
              },
              {
                key: "fetchUserData",
                value: function (t) {
                  this.props.getUserData(t);
                },
              },
            ]),
            n
          );
        })(o.a.Component),
        K = Object(A.connect)(function (t, e) {
          var n = T.d(t, l.q.Illust, e.idOrSecret),
            i = n && Object(u.e)(t, n.userId);
          return {
            client: Object(L.d)(t),
            illustData: n,
            currentWorkId: Object(T.i)(e.idOrSecret)
              ? null == n
                ? void 0
                : n.id
              : e.idOrSecret.id,
            userData: i,
            isLoggedIn: Object(u.r)(t),
            comicPromotion: n ? G.b(t, G.a.Comic, l.q.Illust, n.id) : null,
            fanboxPromotion: n ? G.b(t, G.a.Fanbox, l.q.Illust, n.id) : null,
          };
        }, X)($),
        Q = y.default.div.withConfig({ componentId: "sc-12n125f-0" })([
          "margin:24px -6px 24px -6px;",
        ]),
        J = (n(29), n(53), n(81), n(164), n(7)),
        tt = n.n(J),
        et = (n(39), n(12)),
        nt = n(564),
        it = n(41),
        rt = n(1372),
        ot = n(1373),
        ct = n(414),
        at = n(1457),
        st = n(1296),
        lt = n(40),
        ut = n(1454),
        dt = n(101),
        bt = n(236),
        ht = n(424),
        ft = n(467),
        jt = n(42),
        pt = n(298),
        Ot = n(1389),
        mt = n(1325),
        vt = n(1463),
        gt = (n(176), n(69)),
        xt = n(86),
        yt = n(114),
        wt = n(1497),
        kt = n(14),
        Ct = n(64);
      var It = Object(A.connect)(function (t, e) {
        var n = e.workType,
          i = e.id,
          r = Object(T.c)(t, n, i);
        return {
          seriesUserId: null == r ? void 0 : r.userId,
          url: r && Object(T.f)(r),
          seriesNav: r && r.seriesNav,
        };
      })(function (t) {
        var e = this,
          n = Object(Ct.h)(),
          r = Object(C.g)(
            function () {
              return Object(f.a)(this, e), !!n && t.seriesUserId !== n.id;
            }.bind(this),
            [t, n]
          ),
          o = Object(xt.a)("ab_manga_watchlist_dev"),
          c = Object(w.p)([w.m.func.ns, w.m.following.ns]),
          a = Object(h.a)(c, 1)[0];
        if (
          void 0 === t.url ||
          void 0 === t.seriesNav ||
          null === t.seriesNav ||
          void 0 === t.seriesUserId
        )
          return null;
        var s =
            t.seriesNav.seriesType === l.m.Manga
              ? l.q.Illust
              : t.seriesNav.seriesType === l.m.Novel
              ? l.q.Novel
              : Object(k.v)(t.seriesNav),
          u = t.url,
          d = t.seriesNav,
          b = t.seriesNav,
          j = b.order,
          p = b.prev,
          O = b.next;
        return Object(i.jsxs)(Dt, {
          children: [
            Object(i.jsx)(Rt, {
              children:
                d.seriesType === l.m.Novel
                  ? Object(i.jsx)(_t, {
                      order: j,
                      next: O,
                      workType: s,
                      concluded: d.isConcluded,
                      disabled: null !== d.next && !d.next.available,
                    })
                  : Object(i.jsx)(_t, {
                      order: j,
                      next: O,
                      workType: s,
                      concluded: !1,
                    }),
            }),
            Object(i.jsxs)(Rt, {
              children: [
                Object(i.jsx)(St, {
                  prev: p,
                  workType: s,
                  disabled:
                    d.seriesType === l.m.Novel &&
                    null !== d.prev &&
                    !d.prev.available,
                }),
                Object(i.jsx)(Pt, { url: u, workType: s }),
              ],
            }),
            o &&
              r &&
              Object(i.jsx)("div", {
                className: "gtm-manga-detail-watch-button",
                children: Object(i.jsxs)(qt, {
                  children: [
                    Object(i.jsx)(wt.a, {
                      kind: l.p.Manga,
                      seriesId: d.seriesId,
                      fixed: !0,
                    }),
                    Object(i.jsx)(Tt, {
                      children: a(
                        w.m.following.watchlist.tooltip.after_watched
                      ),
                    }),
                  ],
                }),
              }),
          ],
        });
      });
      function _t(t) {
        var e = t.order,
          n = t.next,
          r = t.workType,
          o = t.concluded,
          c = t.disabled,
          a = void 0 !== c && c,
          s = Object(w.p)(w.m.view.ns),
          u = Object(h.a)(s, 1)[0];
        return null !== n
          ? a
            ? Object(i.jsxs)(Mt, {
                "aria-disabled": !0,
                children: ["#", n.order, " ", n.title],
              })
            : Object(i.jsxs)(Nt, {
                to:
                  r === l.q.Illust
                    ? kt.memberIllust.illust(
                        {},
                        { mode: "medium", illust_id: n.id }
                      )
                    : r === l.q.Novel
                    ? kt.novel.show({}, { id: n.id })
                    : Object(k.v)(r),
                title: n.title,
                className: "gtm-series-next-work-button-in-illust-detail",
                children: ["#", n.order, " ", n.title],
              })
          : Object(i.jsx)(Mt, {
              "aria-disabled": !0,
              children: o
                ? u(w.m.view.series.series_completed)
                : u(w.m.view.series.content_order_is_not_posted, {
                    contentOrder: e + 1,
                  }),
            });
      }
      function St(t) {
        var e = t.prev,
          n = t.workType,
          r = t.disabled;
        return null === e
          ? null
          : void 0 !== r && r
          ? Object(i.jsxs)(Mt, {
              "aria-disabled": !0,
              children: ["#", e.order, " ", e.title],
            })
          : Object(i.jsxs)(Nt, {
              to:
                n === l.q.Illust
                  ? kt.memberIllust.illust(
                      {},
                      { mode: "medium", illust_id: e.id }
                    )
                  : n === l.q.Novel
                  ? kt.novel.show({}, { id: e.id })
                  : Object(k.v)(n),
              title: e.title,
              className: "gtm-series-before-work-button-in-illust-detail",
              children: ["#", e.order, " ", e.title],
            });
      }
      function Pt(t) {
        var e = t.url,
          n = (t.workType, Object(w.p)(w.m.action.ns)),
          r = Object(h.a)(n, 1)[0];
        return (
          l.q.Novel,
          Object(i.jsx)(Nt, {
            to: e,
            className: "gtm-series-list-in-illust-detail",
            children: r(w.m.action.link.see_all_series),
          })
        );
      }
      var Dt = y.default.div.withConfig({ componentId: "jnz8is-0" })([
          "display:grid;grid-auto-flow:row;grid-row-gap:12px;max-width:600px;padding:12px;margin:16px auto 0;",
        ]),
        Tt = y.default.div.withConfig({ componentId: "jnz8is-1" })(
          ["margin:0 auto;text-align:center;", ""],
          Object(gt.a)(
            function (t) {
              return (
                Object(f.a)(this, void 0),
                [t.margin.top(4), t.font.text3, t.typography(14)]
              );
            }.bind(void 0)
          )
        ),
        Rt = y.default.div.withConfig({ componentId: "jnz8is-2" })([
          "display:grid;grid-template-columns:repeat(auto-fit,minmax(0,1fr));column-gap:8px;",
        ]),
        zt = Object(y.css)([
          "flex:1;padding:9px 24px;border-radius:100em;text-align:center;font-weight:bold;white-space:nowrap;text-overflow:ellipsis;overflow:hidden;",
        ]),
        Nt = Object(y.default)(yt.a).withConfig({ componentId: "jnz8is-3" })(
          ["", " ", " &[aria-disabled]{font-weight:normal;}"],
          function (t) {
            return Object(f.a)(this, void 0), t.theme.ui.element.default;
          }.bind(void 0),
          zt
        ),
        Mt = Nt.withComponent("div"),
        qt = y.default.div.withConfig({ componentId: "jnz8is-4" })(
          ["margin:0 auto;width:392px;", ""],
          Object(gt.a)(
            function (t) {
              return Object(f.a)(this, void 0), [t.margin.top(40)];
            }.bind(void 0)
          )
        ),
        Ut = n(1345),
        Et = n(1343),
        Lt = n(268),
        At = n(525),
        Ft = n(309);
      function Bt() {
        var t = this,
          e = (function () {
            var t = this,
              e = Object(s.b)();
            return [
              Object(s.c)(
                function (e) {
                  return Object(f.a)(this, t), Object(ct.d)(e);
                }.bind(this)
              ),
              Object(C.a)(
                function (n) {
                  Object(f.a)(this, t), e(Object(ct.i)(n));
                }.bind(this),
                [e]
              ),
            ];
          })(),
          n = Object(h.a)(e, 2),
          i = n[0],
          r = n[1];
        return [
          i,
          Object(C.a)(
            function (e) {
              Object(f.a)(this, t), r(e);
            }.bind(this),
            [r]
          ),
          Object(C.a)(
            function () {
              Object(f.a)(this, t), r(1);
            }.bind(this),
            [r]
          ),
        ];
      }
      function Wt() {
        return Object(s.c)(ct.h);
      }
      n(52), n(85), n(54);
      var Ht = n(364),
        Vt = n(288),
        Gt = n(347),
        Zt = n(390),
        Yt = n(232),
        Xt = n(382);
      function $t(t) {
        var e = this,
          n = t.id,
          r = Object(s.b)(),
          o = Object(s.c)(
            function (t) {
              return Object(f.a)(this, e), Xt.d(t, l.q.Illust, n);
            }.bind(this)
          ),
          c = Xt.j(l.q.Illust, n),
          a = Object(s.c)(ct.e),
          u = Object(w.p)(w.m.view.ns),
          d = Object(h.a)(u, 1)[0],
          b = Object(Gt.d)(a, null, {
            from: { height: "0px", opacity: 0 },
            enter: { height: "180px", opacity: 1 },
            config: Vt.a,
          }),
          j = Object(C.g)(
            function () {
              var t = this;
              return (
                Object(f.a)(this, e),
                null !== o &&
                  o.length > 0 &&
                  o
                    .filter(
                      function (e) {
                        return Object(f.a)(this, t), !e.isAdContainer;
                      }.bind(this)
                    )
                    .slice(0, 20)
                    .map(
                      function (e) {
                        if ((Object(f.a)(this, t), e.isAdContainer))
                          return null;
                        var n = null == c ? void 0 : c[e.id],
                          r = {
                            zone: l.c.SlideinBooster,
                            recommendScore: null == n ? void 0 : n.score,
                            recommendMethod:
                              null == n ? void 0 : n.methods.join(","),
                            recommendSeedIllustIds:
                              null == n ? void 0 : n.seedIllustIds.join(","),
                          };
                        return e.type === l.q.Illust
                          ? Object(i.jsx)(
                              v.d,
                              {
                                type: l.q.Illust,
                                size: g.c.Size136,
                                thumbnail: e,
                                gtmRecommendData: r,
                                hideTitle: !0,
                              },
                              e.id
                            )
                          : null;
                      }.bind(this)
                    )
                    .filter(Ht.isPresent)
              );
            }.bind(this),
            [o, c]
          );
        return (
          Object(C.d)(
            function () {
              Object(f.a)(this, e), null === o && r(Xt.g(l.q.Illust, n, 20));
            }.bind(this),
            [o, r, n]
          ),
          !1 === j || 0 === j.length
            ? null
            : Object(i.jsx)(i.Fragment, {
                children: b.map(
                  function (t) {
                    Object(f.a)(this, e);
                    var n = t.item,
                      r = t.props,
                      o = t.key;
                    return (
                      n &&
                      Object(i.jsxs)(
                        Kt,
                        {
                          style: r,
                          children: [
                            Object(i.jsx)(Qt, {
                              children: Object(i.jsx)(Jt, {
                                children: d(
                                  w.m.view.discovery.recommended_work
                                ),
                              }),
                            }),
                            Object(i.jsx)(p.a, {
                              children: Object(i.jsx)(te, { children: j }),
                            }),
                          ],
                        },
                        o
                      )
                    );
                  }.bind(this)
                ),
              })
        );
      }
      var Kt = Object(y.default)(Zt.a.div).withConfig({
          componentId: "a4p1vl-0",
        })(["margin-top:8px;padding-bottom:12px;"]),
        Qt = y.default.div.withConfig({ componentId: "a4p1vl-1" })([
          "max-width:600px;margin:0 auto;",
        ]),
        Jt = y.default.h2.withConfig({ componentId: "a4p1vl-2" })(
          ["font-weight:bold;font-size:14px;line-height:22px;color:", ";"],
          function (t) {
            return Object(f.a)(this, void 0), t.theme.materials.text1;
          }.bind(void 0)
        ),
        te = y.default.div.withConfig({ componentId: "a4p1vl-3" })(
          [
            "display:flex;padding:0 ",
            "px;@media (",
            "){padding:0 ",
            "px;}& > * + *{margin-left:8px;}",
          ],
          (Yt.i.default - 600) / 2,
          function (t) {
            return Object(f.a)(this, void 0), t.theme.media.desktopMedium;
          }.bind(void 0),
          (Yt.i.medium - 600) / 2
        ),
        ee = n(125),
        ne = n(297);
      n(70), n(479);
      var ie,
        re,
        oe = function (t) {
          return Object(i.jsxs)(ce, {
            viewBox: "0 0 24 24",
            style: { width: t.size, height: t.size },
            children: [
              Object(i.jsx)(ae, { cx: "12", cy: "12", r: "10" }),
              Object(i.jsx)("path", {
                d:
                  "M9,8 L10,8 C10.5522847,8 11,8.44771525 11,9 L11,15 C11,15.5522847 10.5522847,16 10,16 L9,16\nC8.44771525,16 8,15.5522847 8,15 L8,9 C8,8.44771525 8.44771525,8 9,8 Z M14,8 L15,8\nC15.5522847,8 16,8.44771525 16,9 L16,15 C16,15.5522847 15.5522847,16 15,16 L14,16\nC13.4477153,16 13,15.5522847 13,15 L13,9 C13,8.44771525 13.4477153,8 14,8 Z",
              }),
            ],
          });
        },
        ce = y.default.svg.withConfig({ componentId: "sc-1w3e579-0" })(
          [
            "stroke:none;fill:",
            ";line-height:0;font-size:0;vertical-align:middle;",
          ],
          function (t) {
            return Object(f.a)(this, void 0), t.theme.text.highContrast;
          }.bind(void 0)
        ),
        ae = y.default.circle.withConfig({ componentId: "sc-1w3e579-1" })([
          "fill:#000;fill-opacity:0.4;",
        ]),
        se = n(356);
      function le(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function ue(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? le(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : le(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function de(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(U.a)(t);
          if (e) {
            var r = Object(U.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(q.a)(this, n);
        };
      }
      var be = (function (t) {
          Object(M.a)(r, t);
          var e = de(r);
          function r() {
            var t,
              i = this;
            Object(R.a)(this, r);
            for (var o = arguments.length, c = new Array(o), a = 0; a < o; a++)
              c[a] = arguments[a];
            return (
              (t = e.call.apply(e, [this].concat(c))),
              Object(E.a)(Object(N.a)(t), "state", { ready: !1, paused: !1 }),
              Object(E.a)(Object(N.a)(t), "ref", void 0),
              Object(E.a)(Object(N.a)(t), "zipPlayer", void 0),
              Object(E.a)(
                Object(N.a)(t),
                "rewind",
                function () {
                  Object(f.a)(this, i),
                    void 0 !== t.zipPlayer && t.zipPlayer.rewind();
                }.bind(this)
              ),
              Object(E.a)(
                Object(N.a)(t),
                "saveRef",
                function (e) {
                  Object(f.a)(this, i), (t.ref = e);
                }.bind(this)
              ),
              Object(E.a)(
                Object(N.a)(t),
                "handleClickPlayButton",
                function (e) {
                  var n = this;
                  Object(f.a)(this, i),
                    e.stopPropagation(),
                    void 0 !== t.zipPlayer &&
                      t.setState(
                        function (t) {
                          return Object(f.a)(this, n), { paused: !t.paused };
                        }.bind(this)
                      );
                }.bind(this)
              ),
              Object(E.a)(
                Object(N.a)(t),
                "importZipPlayer",
                function () {
                  var e = this;
                  Object(f.a)(this, i),
                    void 0 === t.props.meta ||
                      t.state.ready ||
                      (void 0 !== re
                        ? t.setState({ ready: !0 })
                        : (void 0 === ie &&
                            (ie = n.e(5).then(n.bind(null, 1442))),
                          ie.then(
                            function (n) {
                              Object(f.a)(this, e),
                                (re = n.default),
                                t.setState({ ready: !0 });
                            }.bind(this)
                          )));
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(z.a)(r, [
              {
                key: "componentDidMount",
                value: function () {
                  this.importZipPlayer();
                },
              },
              {
                key: "componentDidUpdate",
                value: function (t, e) {
                  var n = this.props,
                    i = n.idOrSecret,
                    r = n.meta,
                    o = n.original,
                    c = this.state,
                    a = c.paused,
                    l = c.ready;
                  void 0 === this.zipPlayer ||
                    (Object(s.a)(i, t.idOrSecret) && l) ||
                    (this.zipPlayer.stop(),
                    (this.zipPlayer = void 0),
                    this.setState({ ready: !1 })),
                    void 0 !== this.zipPlayer &&
                      e.paused !== a &&
                      (a ? this.zipPlayer.pause() : this.zipPlayer.play()),
                    this.importZipPlayer(),
                    void 0 !== r &&
                      l &&
                      null !== this.ref &&
                      ((void 0 === this.zipPlayer && void 0 !== re) ||
                        r !== t.meta) &&
                      (this.zipPlayer = new re({
                        canvas: this.ref,
                        source: o ? r.originalSrc : r.src,
                        metadata: ue({}, r),
                        chunkSize: 3e5,
                        loop: !0,
                        autoStart: !0,
                        debug: !1,
                        autosize: !0,
                      }));
                },
              },
              {
                key: "componentWillUnmount",
                value: function () {
                  void 0 !== this.zipPlayer && this.zipPlayer.stop();
                },
              },
              {
                key: "render",
                value: function () {
                  var t,
                    e,
                    n = this.props,
                    r = n.original,
                    o = n.width,
                    c = n.height,
                    a = n.title,
                    s = n.placeholderUrls,
                    l = s.small,
                    u = s.regular,
                    d = s.original,
                    b = n.onClick,
                    h = this.state,
                    f = h.ready,
                    j = h.paused,
                    p = r ? 1920 : 600,
                    O = r ? 1080 : 600;
                  if (o > p || c > O)
                    if (o > c) {
                      var m = c / o;
                      (t = p), (e = Math.round(p * m));
                    } else {
                      var v = o / c;
                      (t = Math.round(O * v)), (e = O);
                    }
                  else (t = o), (e = c);
                  return f
                    ? Object(i.jsxs)(fe, {
                        original: r,
                        onClick: b,
                        children: [
                          Object(i.jsx)(je, {
                            original: r,
                            width: t,
                            height: e,
                            backgroundImage: Object(k.d)(r ? d : l),
                            ref: this.saveRef,
                          }),
                          Object(i.jsx)(pe, {
                            original: r,
                            onClick: this.handleClickPlayButton,
                            children: j
                              ? Object(i.jsx)(se.a, { size: 48 })
                              : Object(i.jsx)(oe, { size: 48 }),
                          }),
                        ],
                      })
                    : Object(i.jsx)(he, {
                        original: r,
                        alt: a,
                        src: l,
                        srcSet: [
                          "".concat(encodeURI(l), " 540w"),
                          encodeURI(u),
                        ].join(","),
                        width: t,
                        height: e,
                        style: { width: t, height: e },
                      });
                },
              },
            ]),
            r
          );
        })(o.a.Component),
        he = y.default.img.withConfig({ componentId: "tu09d3-0" })(
          [
            "display:block;margin:auto;width:",
            ";height:",
            ";max-width:",
            ";background-size:cover;background-repeat:no-repeat;",
          ],
          function (t) {
            Object(f.a)(this, void 0);
            var e = t.width;
            return "".concat(e, "px");
          }.bind(void 0),
          function (t) {
            Object(f.a)(this, void 0);
            var e = t.height;
            return "".concat(e, "px");
          }.bind(void 0),
          function (t) {
            return Object(f.a)(this, void 0), t.original ? "initial" : "100%";
          }.bind(void 0)
        ),
        fe = y.default.div.withConfig({ componentId: "tu09d3-1" })(
          ["position:relative;cursor:", ";"],
          function (t) {
            return (
              Object(f.a)(this, void 0), t.original ? "zoom-out" : "zoom-in"
            );
          }.bind(void 0)
        ),
        je = y.default.canvas.withConfig({ componentId: "tu09d3-2" })(
          [
            "display:block;margin:auto;width:",
            ";height:",
            ";background-image:",
            ";max-width:",
            ";background-size:cover;background-repeat:no-repeat;",
          ],
          function (t) {
            Object(f.a)(this, void 0);
            var e = t.width;
            return "".concat(e, "px");
          }.bind(void 0),
          function (t) {
            Object(f.a)(this, void 0);
            var e = t.height;
            return "".concat(e, "px");
          }.bind(void 0),
          function (t) {
            return Object(f.a)(this, void 0), t.backgroundImage;
          }.bind(void 0),
          function (t) {
            return Object(f.a)(this, void 0), t.original ? "initial" : "100%";
          }.bind(void 0)
        ),
        pe = y.default.button.withConfig({ componentId: "tu09d3-3" })(
          [
            "position:",
            ";bottom:0;right:0;margin:12px;cursor:pointer;border:none;background:none;padding:0;appearance:none;transition:0.2s opacity;opacity:1;&:hover,&:focus{opacity:0.5;}",
          ],
          function (t) {
            return Object(f.a)(this, void 0), t.original ? "fixed" : "absolute";
          }.bind(void 0)
        ),
        Oe = n(312),
        me = o.a.memo(ve);
      function ve(t) {
        var e = t.children,
          n = t.show,
          r = t.onClose;
        return Object(i.jsxs)(Oe.a, {
          show: n,
          dark: !1,
          white: !0,
          onDispose: r,
          onBackgroundClick: r,
          children: [
            Object(i.jsx)(ge, {
              onClick: r,
              children: n && Object(i.jsx)(xe, { children: e }),
            }),
            Object(i.jsx)(ne.default, {
              vk: ["Escape", "v"],
              disabled: !n,
              onShortcut: r,
            }),
          ],
        });
      }
      var ge = y.default.div.withConfig({ componentId: "l1ip84-0" })([
          "position:absolute;top:0;bottom:0;left:0;right:0;overflow:auto;display:flex;flex-flow:column;min-height:100%;cursor:zoom-out;",
        ]),
        xe = y.default.div.withConfig({ componentId: "l1ip84-1" })([
          "margin:auto;> img{width:auto;height:auto;}",
        ]);
      function ye(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function we(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? ye(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : ye(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function ke(t) {
        var e = (function () {
          if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
          if (Reflect.construct.sham) return !1;
          if ("function" == typeof Proxy) return !0;
          try {
            return (
              Date.prototype.toString.call(
                Reflect.construct(Date, [], function () {})
              ),
              !0
            );
          } catch (t) {
            return !1;
          }
        })();
        return function () {
          var n,
            i = Object(U.a)(t);
          if (e) {
            var r = Object(U.a)(this).constructor;
            n = Reflect.construct(i, arguments, r);
          } else n = i.apply(this, arguments);
          return Object(q.a)(this, n);
        };
      }
      var Ce = (function (t) {
          Object(M.a)(r, t);
          var e,
            n = ke(r);
          function r() {
            var t,
              e = this;
            Object(R.a)(this, r);
            for (var i = arguments.length, o = new Array(i), c = 0; c < i; c++)
              o[c] = arguments[c];
            return (
              (t = n.call.apply(n, [this].concat(o))),
              Object(E.a)(Object(N.a)(t), "state", {
                meta: void 0,
                showOriginalModal: !1,
              }),
              Object(E.a)(Object(N.a)(t), "ref", void 0),
              Object(E.a)(
                Object(N.a)(t),
                "saveRef",
                function (n) {
                  Object(f.a)(this, e), (t.ref = n);
                }.bind(this)
              ),
              Object(E.a)(
                Object(N.a)(t),
                "handleThumbnailClose",
                function () {
                  Object(f.a)(this, e), t.setState({ showOriginalModal: !1 });
                }.bind(this)
              ),
              Object(E.a)(
                Object(N.a)(t),
                "handleFullscreen",
                function () {
                  Object(f.a)(this, e), t.setState({ showOriginalModal: !0 });
                }.bind(this)
              ),
              t
            );
          }
          return (
            Object(z.a)(r, [
              {
                key: "componentDidMount",
                value: function () {
                  this.loadUgoiraMeta();
                },
              },
              {
                key: "componentDidUpdate",
                value: function (t, e) {
                  Object(s.a)(this.props.idOrSecret, t.idOrSecret) ||
                    (this.setState({ meta: void 0, showOriginalModal: !1 }),
                    this.loadUgoiraMeta()),
                    e.showOriginalModal &&
                      !this.state.showOriginalModal &&
                      this.ref.rewind();
                },
              },
              {
                key: "render",
                value: function () {
                  return Object(i.jsxs)(_e, {
                    role: "presentation",
                    children: [
                      Object(i.jsx)(
                        be,
                        we(
                          we({}, this.props),
                          {},
                          {
                            meta: this.state.meta,
                            ref: this.saveRef,
                            onClick: this.handleFullscreen,
                          }
                        )
                      ),
                      Object(i.jsx)(ne.default, {
                        vk: "v",
                        onShortcut: this.handleFullscreen,
                      }),
                      this.state.showOriginalModal &&
                        Object(i.jsx)(me, {
                          show: this.state.showOriginalModal,
                          onClose: this.handleThumbnailClose,
                          children: Object(i.jsx)(
                            be,
                            we(
                              we({}, this.props),
                              {},
                              { meta: this.state.meta, original: !0 }
                            )
                          ),
                        }),
                    ],
                  });
                },
              },
              {
                key: "loadUgoiraMeta",
                value:
                  ((e = Object(et.a)(
                    tt.a.mark(function t() {
                      var e, n;
                      return tt.a.wrap(
                        function (t) {
                          for (;;)
                            switch ((t.prev = t.next)) {
                              case 0:
                                return (
                                  (e = this.props.idOrSecret),
                                  (t.next = 3),
                                  Object(T.i)(e)
                                    ? Object(ee.l)(this.props.client, e.secret)
                                    : Object(ee.k)(this.props.client, e.id)
                                );
                              case 3:
                                (n = t.sent), this.setState({ meta: n });
                              case 5:
                              case "end":
                                return t.stop();
                            }
                        },
                        t,
                        this
                      );
                    })
                  )),
                  function () {
                    return e.apply(this, arguments);
                  }),
              },
            ]),
            r
          );
        })(o.a.Component),
        Ie = Object(A.connect)(function (t) {
          return { client: Object(L.d)(t) };
        })(Ce),
        _e = y.default.div.withConfig({ componentId: "sc-7r2pup-0" })(
          [
            "margin:0 0 16px;background-color:",
            ";overflow:hidden;border-radius:8px 8px 0 0;",
          ],
          function (t) {
            return Object(f.a)(this, void 0), t.theme.materials.surface7;
          }.bind(void 0)
        ),
        Se = n(175),
        Pe = n(226),
        De = n(1055),
        Te =
          (n(47),
          n(189),
          n(58),
          n(520),
          n(200),
          n(201),
          n(202),
          n(203),
          n(204),
          n(205),
          n(206),
          n(207),
          n(208),
          n(209),
          n(210),
          n(211),
          n(212),
          n(60),
          n(1271)),
        Re = n(507),
        ze = n.p + "48f26ea47e6bd1b8a3fc6fb44dd68ea7.cur",
        Ne = n.p + "69072b24b3a0d5fe019d4cf45e7bace4.cur";
      function Me(t, e) {
        var n;
        if ("undefined" == typeof Symbol || null == t[Symbol.iterator]) {
          if (
            Array.isArray(t) ||
            (n = (function (t, e) {
              if (!t) return;
              if ("string" == typeof t) return qe(t, e);
              var n = Object.prototype.toString.call(t).slice(8, -1);
              "Object" === n && t.constructor && (n = t.constructor.name);
              if ("Map" === n || "Set" === n) return Array.from(t);
              if (
                "Arguments" === n ||
                /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)
              )
                return qe(t, e);
            })(t)) ||
            (e && t && "number" == typeof t.length)
          ) {
            n && (t = n);
            var i = 0,
              r = function () {};
            return {
              s: r,
              n: function () {
                return i >= t.length
                  ? { done: !0 }
                  : { done: !1, value: t[i++] };
              },
              e: function (t) {
                throw t;
              },
              f: r,
            };
          }
          throw new TypeError(
            "Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."
          );
        }
        var o,
          c = !0,
          a = !1;
        return {
          s: function () {
            n = t[Symbol.iterator]();
          },
          n: function () {
            var t = n.next();
            return (c = t.done), t;
          },
          e: function (t) {
            (a = !0), (o = t);
          },
          f: function () {
            try {
              c || null == n.return || n.return();
            } finally {
              if (a) throw o;
            }
          },
        };
      }
      function qe(t, e) {
        (null == e || e > t.length) && (e = t.length);
        for (var n = 0, i = new Array(e); n < e; n++) i[n] = t[n];
        return i;
      }
      function Ue(t) {
        var e = this,
          n = t.pageCount,
          r = t.fixed,
          o = t.includeBookmarkControl,
          c = t.scrollBarSize,
          a = t.shortcuts,
          s = t.hiddenButtons,
          l = t.pageElements,
          u = t.disabledViewportAdjuster,
          d = t.onMoveToCaption,
          b = Object(Re.b)({ threshold: 0 }),
          j = Object(h.a)(b, 2),
          p = j[0],
          O = j[1],
          m = Bt(),
          v = Object(h.a)(m, 1)[0],
          g = Wt(),
          x = 1 === v,
          y = function (t) {
            Object(f.a)(this, e);
            var n = l.get(t);
            n && Object(Se.g)(n, { block: "start" });
          }.bind(this),
          w = function (t) {
            return Object(f.a)(this, e), y(t);
          }.bind(this),
          k = function () {
            if ((Object(f.a)(this, e), !u)) {
              var t = l.get(v);
              if (t && t.getBoundingClientRect().top <= -1)
                return Object(Se.g)(t, { block: "start" });
            }
            x || w(v - 1);
          }.bind(this),
          C = function () {
            if ((Object(f.a)(this, e), !u)) {
              var t = l.get(v);
              if (t && t.getBoundingClientRect().top >= 1)
                return Object(Se.g)(t, { block: "start" });
            }
            var i = v + 1;
            if (i > n) {
              var r = l.get(n);
              r &&
                Object(Se.g)(r, {
                  offset: r.clientHeight,
                  block: "start",
                  behavior: "smooth",
                }),
                d && d();
            } else w(i);
          }.bind(this),
          I = function (t) {
            Object(f.a)(this, e), t.preventDefault(), t.stopPropagation(), k();
          }.bind(this),
          _ = function (t) {
            Object(f.a)(this, e), t.preventDefault(), t.stopPropagation(), C();
          }.bind(this),
          S = function (t) {
            Object(f.a)(this, e);
            var n = l.get(1);
            (n && n.getBoundingClientRect().top >= 0) ||
              (t.preventDefault(), k());
          }.bind(this),
          P = function (t) {
            Object(f.a)(this, e);
            var i = l.get(n);
            (i && i.getBoundingClientRect().bottom <= 0) ||
              (t.preventDefault(), C());
          }.bind(this);
        return g
          ? Object(i.jsxs)(Ee, {
              ref: p,
              fixed: r,
              children: [
                Object(i.jsx)(Ae, {
                  type: "button",
                  onClick: I,
                  disabled: s || x,
                  fixed: r,
                  scrollBarSize: c,
                }),
                Object(i.jsx)(Fe, {
                  type: "button",
                  onClick: _,
                  disabled: s,
                  fixed: r,
                  scrollBarSize: c,
                  includeBookmarkControl: o,
                }),
                a &&
                  Object(i.jsxs)(i.Fragment, {
                    children: [
                      Object(i.jsx)(ne.default, {
                        disabled: !O,
                        vk: ["k", "ArrowUp"],
                        onShortcut: S,
                      }),
                      Object(i.jsx)(ne.default, {
                        disabled: !O,
                        vk: ["j", "ArrowDown"],
                        onShortcut: P,
                      }),
                    ],
                  }),
              ],
            })
          : null;
      }
      var Ee = y.default.div.withConfig({ componentId: "sc-691snt-0" })(
          [
            "position:absolute;top:0;bottom:",
            ";right:0;left:0;pointer-events:none;",
            "",
          ],
          function (t) {
            return Object(f.a)(this, void 0), t.fixed ? 0 : "-".concat("40vh");
          }.bind(void 0),
          function (t) {
            return (
              Object(f.a)(this, void 0),
              t.fixed && Object(y.css)(["z-index:1;"])
            );
          }.bind(void 0)
        ),
        Le = y.default.button.withConfig({ componentId: "sc-691snt-1" })(
          [
            "position:",
            ";right:0;left:0;margin:0;padding:0;width:",
            ";background:none;outline:none;border:none;opacity:0;pointer-events:auto;",
            " &:disabled{visibility:hidden;}",
          ],
          function (t) {
            return Object(f.a)(this, void 0), t.fixed ? "fixed" : "sticky";
          }.bind(void 0),
          function (t) {
            return (
              Object(f.a)(this, void 0),
              t.fixed && t.scrollBarSize
                ? "calc(100% - ".concat(t.scrollBarSize.width, "px)")
                : "100%"
            );
          }.bind(void 0),
          function (t) {
            return (
              Object(f.a)(this, void 0),
              t.fixed && Object(y.css)(["backface-visibility:hidden;"])
            );
          }.bind(void 0)
        ),
        Ae = Object(y.default)(Le).withConfig({ componentId: "sc-691snt-2" })(
          ["top:0;height:", ";cursor:", " 9 0,", ",pointer;"],
          "20vh",
          Object(k.d)(Ne),
          Object(k.d)(Ne)
        ),
        Fe = Object(y.default)(Le).withConfig({ componentId: "sc-691snt-3" })(
          ["height:", ";cursor:", " 9 15,", ",pointer;", ""],
          "40vh",
          Object(k.d)(ze),
          Object(k.d)(ze),
          function (t) {
            var e = this;
            return (
              Object(f.a)(this, void 0),
              t.fixed
                ? Object(y.css)(
                    ["bottom:", "px;"],
                    function (t) {
                      return (
                        Object(f.a)(this, e),
                        t.scrollBarSize ? t.scrollBarSize.height : 0
                      );
                    }.bind(this)
                  )
                : Object(y.css)(["top:calc(100% - ", ");"], "40vh")
            );
          }.bind(void 0)
        );
      function Be() {
        var t = this,
          e = Bt(),
          n = Object(h.a)(e, 2)[1],
          i = Object(C.i)(new Map()),
          r = Object(C.i)(new Map()),
          o = Object(C.i)(
            new IntersectionObserver(
              function (e) {
                var o;
                Object(f.a)(this, t);
                var c,
                  a = Me(e);
                try {
                  for (a.s(); !(c = a.n()).done; ) {
                    var s = c.value;
                    r.current.set(s.target, s.intersectionRatio);
                  }
                } catch (b) {
                  a.e(b);
                } finally {
                  a.f();
                }
                var l =
                    null !==
                      (o = Object(Te.findCurrentPageWithRatio)(
                        i.current,
                        r.current,
                        0.95
                      )) && void 0 !== o
                      ? o
                      : {},
                  u = l.currentPage,
                  d = l.currentPageRatio;
                u && d && d > 0.05 && n(u);
              }.bind(this),
              { threshold: [0.05, 0.5, 0.95] }
            )
          );
        return [
          Object(C.a)(
            function (e, n) {
              Object(f.a)(this, t), i.current.set(n, e), o.current.observe(n);
            }.bind(this),
            []
          ),
          Object(C.a)(
            function () {
              Object(f.a)(this, t),
                (i.current = new Map()),
                o.current.disconnect();
            }.bind(this),
            []
          ),
        ];
      }
      var We = n(1366),
        He = n(219);
      function Ve(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function Ge(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? Ve(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : Ve(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      var Ze = o.a.memo(Ye);
      function Ye(t) {
        return Object(i.jsx)(Oe.a, {
          show: t.show,
          dark: !1,
          white: !0,
          onDispose: t.onClose,
          onBackgroundClick: t.onClose,
          children: Object(i.jsx)(Xe, Ge({}, t)),
        });
      }
      function Xe(t) {
        var e = this,
          n = t.children,
          r = t.show,
          o = t.pageCount,
          c = t.currentPage,
          a = t.expanded,
          s = t.onClose,
          l = t.onClickCountBadge,
          u = t.pageElements,
          d = t.disablePageControllerAndCountBadge,
          b = Object(C.i)(null),
          h = Object(j.s)(b);
        Object(C.d)(
          function () {
            var t = this;
            return (
              Object(f.a)(this, e),
              function () {
                Object(f.a)(this, t),
                  (b.current.scrollTop = 0),
                  (b.current.scrollLeft = 0);
              }.bind(this)
            );
          }.bind(this),
          [c]
        );
        var p = Object(C.a)(
            function (t) {
              Object(f.a)(this, e);
              var n = 1 === t.deltaMode ? 16 : 1,
                i = t.deltaMode ? n * t.deltaX : t.deltaX,
                r = t.deltaMode ? n * t.deltaY : t.deltaY;
              0 !== r
                ? (b.current.scrollTop += r)
                : 0 !== i && (b.current.scrollLeft += i);
            }.bind(this),
            [c]
          ),
          O = o > 1;
        return Object(i.jsxs)(i.Fragment, {
          children: [
            Object(i.jsx)($e, {
              onClick: s,
              ref: b,
              children: r && Object(i.jsx)(Ke, { children: n }),
            }),
            a &&
              !d &&
              Object(i.jsxs)(i.Fragment, {
                children: [
                  Object(i.jsx)(Je, {
                    onWheel: p,
                    children: Object(i.jsx)(Ue, {
                      pageCount: o,
                      fixed: !0,
                      shortcuts: !0,
                      scrollBarSize: h,
                      disabledViewportAdjuster: !0,
                      pageElements: u,
                      onMoveToCaption: s,
                    }),
                  }),
                  O &&
                    Object(i.jsx)(Qe, {
                      children: Object(i.jsx)(He.b, {
                        value: Math.min(c, o),
                        total: o,
                        onClick: l,
                      }),
                    }),
                ],
              }),
            Object(i.jsx)(ne.default, {
              vk: ["Escape", "v"],
              disabled: !r,
              onShortcut: s,
            }),
          ],
        });
      }
      var $e = y.default.div.withConfig({ componentId: "sc-1pkrz0g-0" })(
          [
            "position:absolute;top:0;bottom:0;left:0;right:0;overflow:auto;display:flex;flex-flow:column;min-height:100%;cursor:zoom-out;background-color:",
            ";",
          ],
          function (t) {
            return Object(f.a)(this, void 0), t.theme.color.background1;
          }.bind(void 0)
        ),
        Ke = y.default.div.withConfig({ componentId: "sc-1pkrz0g-1" })([
          "margin:auto;> img{width:auto;height:auto;}",
        ]),
        Qe = y.default.div.withConfig({ componentId: "sc-1pkrz0g-2" })([
          "position:fixed;top:16px;right:16px;z-index:1;",
        ]),
        Je = y.default.div.withConfig({ componentId: "sc-1pkrz0g-3" })([""]);
      function tn(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function en(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? tn(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : tn(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function nn(t) {
        var e = this,
          n = t.workId,
          o = t.workTitle,
          c = t.workAlt,
          a = t.workType,
          s = t.pageIllust,
          l = Bt(),
          u = Object(h.a)(l, 1)[0],
          d = Object(Ct.d)(),
          b = Object(j.b)(!1),
          p = Object(h.a)(b, 2),
          O = p[0],
          m = p[1],
          v = m.true,
          g = m.false,
          x = Object(j.b)(!1),
          y = Object(h.a)(x, 2),
          w = y[0],
          k = y[1],
          I = k.true,
          _ = k.false,
          S = (function () {
            var t = this,
              e = Object(C.j)(new Map()),
              n = Object(h.a)(e, 2),
              i = n[0],
              o = n[1],
              c = Object(r.useRef)(null);
            return (
              Object(C.d)(
                function () {
                  Object(f.a)(this, t);
                  var e = new Map();
                  c.current && e.set(1, c.current), o(e);
                }.bind(this),
                []
              ),
              [i, c]
            );
          })(),
          P = Object(h.a)(S, 2),
          D = P[0],
          T = P[1],
          R = Be(),
          z = Object(h.a)(R, 2),
          N = z[0],
          M = z[1],
          q = function () {
            if ((Object(f.a)(this, e), !d)) return v();
            I();
          }.bind(this),
          U = function () {
            Object(f.a)(this, e);
            var t = D.get(1);
            t && Object(Se.g)(t, { block: "start" }), q();
          }.bind(this);
        return (
          Object(C.d)(
            function () {
              var t = this;
              Object(f.a)(this, e);
              var n = D.get(1);
              if (n)
                return (
                  N(1, n),
                  function () {
                    return Object(f.a)(this, t), M();
                  }.bind(this)
                );
            }.bind(this),
            [M, N, D]
          ),
          Object(i.jsxs)(rn, {
            role: "presentation",
            children: [
              Object(i.jsx)(on, {
                className: "gtm-medium-work-expanded-view",
                children: Object(i.jsx)(
                  We.default,
                  en(
                    en({}, s),
                    {},
                    {
                      ref: T,
                      page: 1,
                      illustId: n,
                      title: o,
                      alt: c,
                      illustType: a,
                      gtmClass: "expand-full-size-illust",
                      onClick: U,
                      shortcutVk: "v",
                    }
                  )
                ),
              }),
              Object(i.jsx)(Ue, {
                pageCount: 1,
                includeBookmarkControl: !0,
                shortcuts: !0,
                hiddenButtons: !0,
                disabledViewportAdjuster: w,
                pageElements: D,
              }),
              Object(i.jsx)(Ze, {
                show: w,
                currentPage: u,
                pageCount: 1,
                expanded: !0,
                onClose: _,
                pageElements: D,
                children: Object(i.jsx)("img", {
                  src: s.urls.original,
                  alt: c,
                  width: s.width,
                  height: s.height,
                }),
              }),
              Object(i.jsx)(De.default, {
                show: O,
                labelType: Pe.a.EnlargeWorks,
                onClose: g,
              }),
            ],
          })
        );
      }
      var rn = y.default.div.withConfig({ componentId: "sc-166cqri-0" })([
          "position:relative;",
        ]),
        on = y.default.div.withConfig({ componentId: "sc-166cqri-1" })([
          "position:relative;& + &{padding-top:40px;}&:first-child{border-radius:8px 8px 0 0;}",
        ]),
        cn = (n(88), n(99), n(27)),
        an = n(245);
      function sn(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function ln(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? sn(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : sn(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      var un = n(335),
        dn = (function (t, e, n) {
          var r = e.calculatePosition,
            c = e.calculateTipPosition,
            a = e.renderTutorial;
          return (s.displayName = n), o.a.memo(o.a.forwardRef(s));
          function s(e, n) {
            return Object(i.jsx)(
              an.b,
              ln(
                ln(
                  ln({}, t),
                  {},
                  {
                    calculatePosition: r,
                    calculateTipPosition: c,
                    renderTutorial: a,
                  },
                  e
                ),
                {},
                { ref: n }
              )
            );
          }
        })(
          {
            storageKey: "showOnce",
            tutorialId: "previewModal",
            textWidth: 232,
            okButton: an.a.Hidden,
          },
          {
            calculatePosition: function (t) {
              Object(f.a)(this, void 0);
              var e = t.offsetTop,
                n = t.offsetLeft,
                i = t.offsetWidth,
                r = Math.min(n + i / 2 - 144, window.innerWidth - 296);
              return { left: r, right: r, top: e };
            }.bind(void 0),
            calculateTipPosition: function (t, e) {
              Object(f.a)(this, void 0);
              var n = t.offsetLeft,
                i = t.offsetWidth;
              return { top: e.top, left: n + i / 2, direction: un.c.Top };
            }.bind(void 0),
            renderTutorial: function () {
              var t = this;
              return (
                Object(f.a)(this, void 0),
                Object(i.jsx)(w.d, {
                  children: function (e) {
                    return (
                      Object(f.a)(this, t),
                      e(
                        "��꺁�껁궚�쇻굥�ⓦ걲�밤겍��뵽�뤵굮�쀣꺃�볝깷�쇈겎�띲겲��"
                      )
                    );
                  }.bind(this),
                })
              );
            }.bind(void 0),
          },
          "PreviewModalTutorial"
        ),
        bn = n(474);
      function hn(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function fn(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? hn(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : hn(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function jn(t) {
        var e = this,
          n = t.onClick,
          r = Object(bn.e)(),
          o = Object(h.a)(r, 1)[0],
          c = Object(an.c)("showOnce", "previewModal"),
          a = Object(h.a)(c, 2),
          s = a[0],
          l = a[1],
          u = Object(C.a)(
            function () {
              Object(f.a)(this, e), l(), n();
            }.bind(this),
            [l, n]
          );
        return Object(i.jsx)(pn, {
          children: Object(i.jsx)(On, {
            disabled: t.disabled,
            children: Object(i.jsxs)("div", {
              children: [
                Object(i.jsx)(He.b, fn(fn({}, t), {}, { onClick: u })),
                !o &&
                  s &&
                  Object(i.jsx)(dn, {
                    children: function (t) {
                      return (
                        Object(f.a)(this, e), Object(i.jsx)("div", { ref: t })
                      );
                    }.bind(this),
                  }),
              ],
            }),
          }),
        });
      }
      var pn = y.default.div.withConfig({ componentId: "sc-1c2qglr-0" })([
          "position:absolute;top:0;bottom:0;right:0;pointer-events:none;z-index:1;",
        ]),
        On = Object(y.default)(Object(Se.e)("div", ["disabled"])).withConfig({
          componentId: "sc-1c2qglr-1",
        })(
          [
            "position:",
            ";top:0;display:flex;align-items:flex-start;justify-content:flex-end;padding:4px 4px 60px;box-sizing:border-box;> *{pointer-events:auto;}",
          ],
          function (t) {
            return (
              Object(f.a)(this, void 0), t.disabled ? "relative" : "sticky"
            );
          }.bind(void 0)
        ),
        mn = n(303),
        vn = n(105),
        gn = n(163);
      function xn(t) {
        var e = this,
          n = t.show,
          r = t.illustId,
          o = t.pageIllusts,
          c = t.title,
          a = t.illustType,
          s = t.onClose,
          u = t.onClickPagePreview,
          d = Object(T.o)(l.q.Illust, r),
          b = Object(Ct.j)(null == d ? void 0 : d.userId),
          j = Bt(),
          p = Object(h.a)(j, 2),
          O = p[0],
          m = p[1],
          v = Object(w.o)(),
          g = Object(h.a)(v, 1)[0],
          x = function (t) {
            Object(f.a)(this, e), u && u(t), m(t), s();
          }.bind(this);
        return Object(i.jsxs)(vn.a, {
          title: g("�쀣꺃�볝깷��"),
          show: n,
          onClose: s,
          children: [
            void 0 !== o &&
              b &&
              Object(i.jsx)(yn, {
                children: Object(i.jsx)(kn, {
                  children: o.map(
                    function (t, n) {
                      var r = this;
                      Object(f.a)(this, e);
                      var o = n + 1;
                      return Object(i.jsx)(
                        "li",
                        {
                          children: Object(i.jsx)(Cn, {
                            url: t.urls.thumb_mini,
                            onClick: function () {
                              return Object(f.a)(this, r), x(o);
                            }.bind(this),
                            userName: b.name,
                            title: c,
                            illustType: a,
                            disabled: o === O,
                          }),
                        },
                        o
                      );
                    }.bind(this)
                  ),
                }),
              }),
            Object(i.jsx)(ne.default, {
              vk: ["Escape", "z"],
              disabled: !n,
              onShortcut: s,
            }),
          ],
        });
      }
      var yn = y.default.div.withConfig({ componentId: "sc-9kbqbt-0" })([
          "display:flex;justify-content:center;",
        ]),
        wn = g.c.Size128,
        kn = y.default.ul.withConfig({ componentId: "sc-9kbqbt-1" })(
          [
            "padding:0;height:fit-content;margin:24px 40px 40px;list-style:none;display:grid;grid-template-columns:repeat(auto-fit,",
            "px);gap:",
            "px;max-width:",
            "px;",
          ],
          wn,
          8,
          6 * wn + 40
        );
      function Cn(t) {
        var e = this,
          n = t.url,
          r = t.title,
          o = t.userName,
          c = t.illustType,
          a = t.onClick,
          s = t.disabled,
          u = void 0 !== s && s,
          d = function () {
            Object(f.a)(this, e), u || a();
          }.bind(this);
        return Object(i.jsx)(In, {
          onClick: d,
          "aria-disabled": u,
          children: Object(i.jsx)(mn.b, {
            type: l.q.Illust,
            title: r,
            url: n,
            illustType: c,
            userName: o,
            grayMask: !0,
          }),
        });
      }
      var In = y.default.div.withConfig({ componentId: "sc-9kbqbt-2" })(
          [
            "width:",
            "px;height:",
            "px;cursor:pointer;transition:0.2s opacity;",
            "{opacity:0.2;cursor:default;}&:not(",
            "):hover{opacity:0.8;}",
          ],
          g.c.Size128,
          g.c.Size128,
          gn.b,
          gn.b
        ),
        _n = n(23);
      function Sn() {
        var t = Object(it.f)();
        return Object(Te.pageAnchorValue)(t.location.hash);
      }
      function Pn(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function Dn(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? Pn(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : Pn(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function Tn(t) {
        var e = this,
          n = t.onClickExpandToggle,
          r = Object(cn.a)(t, ["onClickExpandToggle"]),
          o = Wt(),
          c = Sn(),
          a = Object(j.b)(!1),
          s = Object(h.a)(a, 2),
          l = s[0],
          u = s[1],
          d = u.true,
          b = u.false;
        return (
          Object(C.d)(
            function () {
              Object(f.a)(this, e), o || (c && c > 1 && n());
            }.bind(this),
            [n, o, c]
          ),
          Object(i.jsx)(i.Fragment, {
            children: o
              ? Object(i.jsx)(
                  Rn,
                  Dn(
                    Dn({}, r),
                    {},
                    {
                      tutorialDisabled: l,
                      onClickOpenViewerCountBadge: d,
                      showPreviewModal: l,
                      onClosePreviewModal: b,
                    }
                  )
                )
              : Object(i.jsxs)(zn, {
                  role: "presentation",
                  children: [
                    Object(i.jsx)(Un, {
                      value: 1,
                      disabled: !0,
                      total: r.pageCount,
                      onClick: function () {
                        Object(f.a)(this, e), n(), d();
                      }.bind(this),
                    }),
                    Object(i.jsx)(Mn, {
                      children: Object(i.jsx)(
                        We.default,
                        Dn(
                          Dn({}, r.pageIllusts[0]),
                          {},
                          {
                            illustId: r.workId,
                            title: r.workTitle,
                            alt: r.workAlt,
                            illustType: r.workType,
                            page: 1,
                            noZoom: !0,
                            onClick: n,
                          }
                        )
                      ),
                    }),
                  ],
                }),
          })
        );
      }
      function Rn(t) {
        var e = this,
          n = t.workId,
          r = t.workTitle,
          c = t.workAlt,
          a = t.workType,
          s = t.pageCount,
          u = t.pageIllusts,
          d = t.tutorialDisabled,
          b = t.onClickOpenViewerCountBadge,
          p = t.showPreviewModal,
          O = t.onClosePreviewModal,
          m = Bt(),
          v = Object(h.a)(m, 1)[0],
          g = (function (t, e) {
            var n = this,
              i = Object(C.i)(
                Object(_n.a)(Array(e)).map(
                  function () {
                    return Object(f.a)(this, n), o.a.createRef();
                  }.bind(this)
                )
              ),
              r = Object(C.j)(new Map()),
              c = Object(h.a)(r, 2),
              a = c[0],
              s = c[1];
            return (
              Object(C.d)(
                function () {
                  var t = this;
                  Object(f.a)(this, n);
                  var e = new Map();
                  i.current.forEach(
                    function (n, i) {
                      if ((Object(f.a)(this, t), n.current)) {
                        var r = i + 1;
                        e.set(r, n.current);
                      }
                    }.bind(this)
                  ),
                    s(e);
                }.bind(this),
                [t]
              ),
              [a, i.current]
            );
          })(u, s),
          x = Object(h.a)(g, 2),
          y = x[0],
          w = x[1],
          k = Be(),
          I = Object(h.a)(k, 2),
          _ = I[0],
          S = I[1],
          P = Sn(),
          D = Object(Ct.d)(),
          T = Object(j.b)(!1),
          R = Object(h.a)(T, 2),
          z = R[0],
          N = R[1],
          M = N.true,
          q = N.false,
          U = Object(j.b)(!1),
          E = Object(h.a)(U, 2),
          L = E[0],
          A = E[1],
          F = A.true,
          B = A.false,
          W = Object(C.a)(
            function () {
              if ((Object(f.a)(this, e), !D)) return M();
              F();
            }.bind(this),
            [F, D, M]
          ),
          H = Object(C.a)(
            function (t) {
              Object(f.a)(this, e);
              var n = y.get(t);
              n && Object(Se.g)(n, { block: "start" }), W();
            }.bind(this),
            [W, y]
          ),
          V = Object(C.a)(
            function (t) {
              Object(f.a)(this, e);
              var n = y.get(t);
              n && Object(Se.g)(n, { block: "start" });
            }.bind(this),
            [y]
          );
        Object(C.d)(
          function () {
            var t = this;
            return (
              Object(f.a)(this, e),
              y.forEach(
                function (e, n) {
                  Object(f.a)(this, t), _(n, e);
                }.bind(this)
              ),
              function () {
                return Object(f.a)(this, t), S();
              }.bind(this)
            );
          }.bind(this),
          [S, _, y]
        ),
          Object(C.d)(
            function () {
              if ((Object(f.a)(this, e), y.size === s))
                if (P) {
                  var t = y.get(P);
                  t && Object(Se.g)(t, { block: "start", behavior: "smooth" });
                } else {
                  var n = y.get(1);
                  n && Object(Se.g)(n, { block: "start", behavior: "smooth" });
                }
            }.bind(this),
            [P, s, y]
          );
        var G = v - 1,
          Z = u[G] ? u[G] : null,
          Y =
            u.filter(
              function (t) {
                return Object(f.a)(this, e), t.height > 3 * t.width;
              }.bind(this)
            ).length >=
            u.length / 2;
        return Object(i.jsxs)(zn, {
          role: "presentation",
          children: [
            !Y && Object(i.jsx)(Un, { value: v, total: s, onClick: b }),
            u.map(
              function (t, o) {
                Object(f.a)(this, e);
                var l = o + 1,
                  u = v === l;
                return Object(i.jsx)(
                  Nn,
                  Dn(
                    Dn(
                      {
                        gtmClass:
                          1 === l
                            ? "gtm-medium-work-expanded-view"
                            : l === s
                            ? "gtm-illust-work-scroll-finish-reading"
                            : void 0,
                      },
                      t
                    ),
                    {},
                    {
                      ref: w[o],
                      page: l,
                      illustId: n,
                      title: r,
                      alt: c,
                      illustType: a,
                      onClick: H,
                      shortcutEnabled: !L && u,
                      forceOriginalSize: Y,
                    }
                  ),
                  t.urls.regular
                );
              }.bind(this)
            ),
            Object(i.jsx)(o.a.Suspense, {
              fallback: null,
              children: Object(i.jsx)(bn.c, { tutorialDisabled: d || Y }),
            }),
            !Y &&
              Object(i.jsx)(Ue, {
                pageCount: s,
                includeBookmarkControl: !0,
                shortcuts: !0,
                pageElements: y,
                disabledViewportAdjuster: L,
              }),
            Z &&
              Object(i.jsx)(Ze, {
                show: L,
                currentPage: v,
                pageCount: s,
                expanded: !0,
                onClose: B,
                onClickCountBadge: b,
                pageElements: y,
                disablePageControllerAndCountBadge: Y,
                children: Object(i.jsx)(
                  "img",
                  {
                    src: Z.urls.original,
                    alt: c,
                    width: Z.width,
                    height: Z.height,
                  },
                  v
                ),
              }),
            Object(i.jsx)(xn, {
              illustId: n,
              pageIllusts: u,
              title: r,
              illustType: l.t.fromIllustType(a),
              show: p,
              onClose: O,
              onClickPagePreview: V,
            }),
            Object(i.jsx)(De.default, {
              show: z,
              labelType: Pe.a.EnlargeWorks,
              onClose: q,
            }),
          ],
        });
      }
      var zn = y.default.div.withConfig({ componentId: "sc-1qi8blb-0" })([
          "position:relative;",
        ]),
        Nn = o.a.memo(
          o.a.forwardRef(function (t, e) {
            var n = this,
              r = t.gtmClass,
              o = t.onClick,
              c = t.shortcutEnabled,
              a = void 0 === c || c,
              s = Object(cn.a)(t, ["gtmClass", "onClick", "shortcutEnabled"]),
              l = function (t, e) {
                return Object(f.a)(this, n), o(e);
              }.bind(this);
            return Object(i.jsxs)(Mn, {
              className: r,
              children: [
                Object(i.jsx)(qn, { id: "".concat(s.page) }),
                Object(i.jsx)(
                  We.default,
                  Dn(
                    Dn({}, s),
                    {},
                    {
                      ref: e,
                      gtmClass: "expand-full-size-illust",
                      onClick: l,
                      shortcutVk: a ? "v" : void 0,
                    }
                  )
                ),
              ],
            });
          })
        ),
        Mn = y.default.div.withConfig({ componentId: "sc-1qi8blb-1" })([
          "position:relative;& + &{padding-top:40px;}&:first-child{border-radius:8px 8px 0 0;}",
        ]),
        qn = y.default.div.withConfig({ componentId: "sc-1qi8blb-2" })([
          "height:0;visibility:hidden;",
        ]);
      function Un(t) {
        return Object(i.jsxs)(i.Fragment, {
          children: [
            Object(i.jsx)(jn, Dn({}, t)),
            Object(i.jsx)(ne.default, { vk: "z", onShortcut: t.onClick }),
          ],
        });
      }
      function En(t) {
        var e = this,
          n = t.workId,
          r = t.workTitle,
          o = t.workAlt,
          c = t.workType,
          a = t.pageIllusts,
          s = t.pageCount,
          l = t.onClickExpandToggle,
          u = Bt(),
          d = Object(h.a)(u, 3)[2];
        return (
          Object(C.d)(
            function () {
              Object(f.a)(this, e), d();
            }.bind(this),
            [d, n]
          ),
          s > 1
            ? Object(i.jsx)(Tn, {
                workId: n,
                workTitle: r,
                workAlt: o,
                workType: c,
                pageIllusts: a,
                pageCount: s,
                onClickExpandToggle: l,
              })
            : Object(i.jsx)(nn, {
                workId: n,
                workTitle: r,
                workAlt: o,
                workType: c,
                pageIllust: a[0],
              })
        );
      }
      function Ln(t, e) {
        var n = Object.keys(t);
        if (Object.getOwnPropertySymbols) {
          var i = Object.getOwnPropertySymbols(t);
          e &&
            (i = i.filter(function (e) {
              return Object.getOwnPropertyDescriptor(t, e).enumerable;
            })),
            n.push.apply(n, i);
        }
        return n;
      }
      function An(t) {
        for (var e = 1; e < arguments.length; e++) {
          var n = null != arguments[e] ? arguments[e] : {};
          e % 2
            ? Ln(Object(n), !0).forEach(function (e) {
                Object(E.a)(t, e, n[e]);
              })
            : Object.getOwnPropertyDescriptors
            ? Object.defineProperties(t, Object.getOwnPropertyDescriptors(n))
            : Ln(Object(n)).forEach(function (e) {
                Object.defineProperty(
                  t,
                  e,
                  Object.getOwnPropertyDescriptor(n, e)
                );
              });
        }
        return t;
      }
      function Fn(t) {
        var e = this,
          n = t.idOrSecret,
          r = t.illustId,
          o = t.tagsEditable,
          c = t.onTagEditClick,
          a = Object(s.c)(
            function (t) {
              return Object(f.a)(this, e), Object(T.c)(t, l.q.Illust, r);
            }.bind(this)
          );
        return a
          ? Object(i.jsx)(Bn, {
              idOrSecret: n,
              illustId: r,
              tagsEditable: o,
              onTagEditClick: c,
              illustData: a,
            })
          : null;
      }
      function Bn(t) {
        var e = this,
          n = t.idOrSecret,
          r = t.illustId,
          o = t.tagsEditable,
          c = t.onTagEditClick,
          a = t.illustData,
          d = Object(s.c)(
            function (t) {
              return (
                Object(f.a)(this, e),
                Object(k.e)(Object(Ft.c)(t, l.q.Illust, r))
              );
            }.bind(this)
          ),
          b = Object(jt.m)();
        return Object(i.jsx)(Wn, {
          idOrSecret: n,
          illustId: r,
          tagsEditable: o,
          onTagEditClick: c,
          client: Object(s.c)(L.d),
          illustData: a,
          userData: Object(s.c)(
            function (t) {
              return Object(f.a)(this, e), Object(u.e)(t, a.userId);
            }.bind(this)
          ),
          descriptionBoothId: Object(s.c)(
            function (t) {
              return (
                Object(f.a)(this, e),
                Object(k.e)(Object(Lt.d)(t, l.q.Illust, r))
              );
            }.bind(this)
          ),
          imageResponseOutData: Object(s.c)(
            function (t) {
              return (
                Object(f.a)(this, e),
                Object(k.e)(Object(At.c)(t, l.q.Illust, r))
              );
            }.bind(this)
          ),
          tags: d.tags.map(
            function (t) {
              return Object(f.a)(this, e), t.tag;
            }.bind(this)
          ),
          selfAdult: Object(s.c)(u.h),
          selfXRestrict: l.t.toXRestrict(Object(s.c)(u.l)),
          isMuted: Object(s.c)(
            function (t) {
              return Object(f.a)(this, e), Object(dt.i)(t, a, d);
            }.bind(this)
          ),
          viewerUserId: Object(s.c)(u.k),
          isLoggedIn: Object(s.c)(u.r),
          profileImg: Object(s.c)(u.j),
          viewerExpanded: Object(s.c)(
            function (t) {
              return (
                Object(f.a)(this, e),
                (b.pageType === l.j.Illust ||
                  b.pageType === l.j.IllustUnlisted) &&
                  Object(ct.h)(t)
              );
            }.bind(this)
          ),
          pageLoadBusy: Object(s.c)(ct.g),
        });
      }
      var Wn = o.a.memo(function (t) {
          var e = this,
            n = t.idOrSecret,
            r = t.illustId,
            o = t.illustData,
            d = t.userData,
            b = t.isMuted,
            h = t.tags,
            j = t.tagsEditable,
            p = t.descriptionBoothId,
            O = t.selfXRestrict,
            m = t.selfAdult,
            v = t.isLoggedIn,
            g = t.imageResponseOutData,
            x = t.onTagEditClick,
            y = t.profileImg,
            w = t.client,
            k = t.pageLoadBusy,
            I = t.viewerExpanded,
            _ = t.viewerUserId,
            S = Object(C.i)(null),
            P = Object(C.i)(void 0),
            D = Object(s.b)();
          Object(C.d)(
            function () {
              Object(f.a)(this, e), void 0 === d && D(Object(u.m)(o.userId));
            }.bind(this),
            [D, o.userId, d]
          ),
            Object(C.d)(
              function () {
                Object(f.a)(this, e),
                  P.current &&
                    P.current.userId !== o.userId &&
                    void 0 === d &&
                    D(Object(u.m)(o.userId)),
                  (P.current = o);
              }.bind(this),
              [D, o, P, d]
            );
          var R = Object(C.a)(
              (function () {
                var t = Object(et.a)(
                  tt.a.mark(function t(e) {
                    var n;
                    return tt.a.wrap(function (t) {
                      for (;;)
                        switch ((t.prev = t.next)) {
                          case 0:
                            return (
                              (t.next = 2), Object(st.g)(w, r, o.userId, e)
                            );
                          case 2:
                            (n = t.sent),
                              S.current &&
                                S.current.injectComment({
                                  userId: n.user_id,
                                  userName: n.user_name,
                                  img: y || "",
                                  id: "".concat(n.comment_id),
                                  comment: n.comment,
                                  stampId: n.stamp_id,
                                  commentParentId: null,
                                  commentDate: Object(lt.default)().format(),
                                  commentUserId: n.user_id,
                                  editable: !0,
                                  hasReplies: !1,
                                });
                          case 4:
                          case "end":
                            return t.stop();
                        }
                    }, t);
                  })
                );
                return function (e) {
                  return t.apply(this, arguments);
                };
              })(),
              [w, r, o.userId, y]
            ),
            z = Object(C.a)(
              (function () {
                var t = Object(et.a)(
                  tt.a.mark(function t(e) {
                    var n, i;
                    return tt.a.wrap(function (t) {
                      for (;;)
                        switch ((t.prev = t.next)) {
                          case 0:
                            return (
                              (n = e.id),
                              (t.next = 3),
                              Object(st.k)(w, r, o.userId, n)
                            );
                          case 3:
                            (i = t.sent),
                              S.current &&
                                S.current.injectComment({
                                  userId: i.user_id,
                                  userName: i.user_name,
                                  img: y || "",
                                  id: "".concat(i.comment_id),
                                  comment: i.comment,
                                  stampId: i.stamp_id,
                                  commentParentId: null,
                                  commentDate: Object(lt.default)().format(),
                                  commentUserId: i.user_id,
                                  editable: !0,
                                  hasReplies: !1,
                                });
                          case 5:
                          case "end":
                            return t.stop();
                        }
                    }, t);
                  })
                );
                return function (e) {
                  return t.apply(this, arguments);
                };
              })(),
              [w, r, o.userId, y]
            ),
            N = Object(C.a)(
              (function () {
                var t = Object(et.a)(
                  tt.a.mark(function t(e) {
                    return tt.a.wrap(function (t) {
                      for (;;)
                        switch ((t.prev = t.next)) {
                          case 0:
                            if (!k) {
                              t.next = 2;
                              break;
                            }
                            return t.abrupt("return");
                          case 2:
                            return (
                              e && e.preventDefault(),
                              I || D(Object(ct.j)(!0)),
                              t.abrupt("return", D(Object(ct.c)(r, n)))
                            );
                          case 5:
                          case "end":
                            return t.stop();
                        }
                    }, t);
                  })
                );
                return function (e) {
                  return t.apply(this, arguments);
                };
              })(),
              [k, I, D, r, n]
            ),
            M = o.userId,
            q = _ || "0";
          return Object(i.jsxs)(i.Fragment, {
            children: [
              Object(i.jsx)(a.c, {
                children: Object(i.jsx)(
                  Vn,
                  An(
                    An({}, o),
                    {},
                    {
                      idOrSecret: n,
                      tags: h,
                      isMuted: b,
                      tagsEditable: j,
                      onTagEditClick: x,
                      onExpandClick: N,
                      descriptionBoothId: p,
                      selfXRestrict: O,
                      selfAdult: m,
                    }
                  )
                ),
              }),
              Object(i.jsxs)(Hn, {
                children: [
                  !Object(T.i)(n) &&
                    null !== g &&
                    g.length > 0 &&
                    Object(i.jsx)(Kn, {
                      children: Object(i.jsx)(vt.a, {
                        imageResponseOutData: g,
                      }),
                    }),
                  I &&
                    o.pageCount > 1 &&
                    Object(i.jsx)(Zn, {
                      children: Object(i.jsx)(c.c, {
                        kind: c.a.ExpandedFooter,
                      }),
                    }),
                  !Object(T.i)(n) &&
                    Object(i.jsxs)(i.Fragment, {
                      children: [
                        Object(i.jsx)(a.c, {
                          children: Object(i.jsx)(Ot.a, {
                            workType: l.q.Illust,
                            id: o.id,
                            userId: o.userId,
                            userName: d ? d.name : "",
                            userImage: d ? d.profileImg : "",
                          }),
                        }),
                        Object(i.jsx)(bt.b, {
                          controlKey: bt.a.Comment,
                          props: { illustData: o },
                          children: Object(i.jsx)(a.c, {
                            children: Object(i.jsx)(Qn, {
                              children: Object(i.jsx)(Kn, {
                                children: Object(i.jsx)(ut.a, {
                                  ref: S,
                                  creatorUserId: M,
                                  viewerUserId: q,
                                  illustId: o.id,
                                  isMyself: M === q,
                                  notAuthorized: !1,
                                  disabledForm: !v,
                                  profileImage: y || "",
                                  onSubmit: R,
                                  onSubmitStamp: z,
                                }),
                              }),
                            }),
                          }),
                        }),
                      ],
                    }),
                ],
              }),
            ],
          });
        }),
        Hn = y.default.div.withConfig({ componentId: "sc-1yvhotl-0" })([
          "position:relative;margin:0 -16px;",
        ]);
      function Vn(t) {
        var e = this,
          n = t.id,
          r = t.alt,
          o = t.pageCount,
          c = Object(ht.b)(ht.a.ForEnglish),
          a = Object(s.c)(ct.e),
          u = Object(C.i)(null),
          d = Object(pt.a)(t).title,
          b = Object(T.i)(t.idOrSecret),
          h = (function (t) {
            var e = this;
            return Object(s.c)(
              function (n) {
                return Object(f.a)(this, e), Object(ct.a)(n, t);
              }.bind(this)
            );
          })(n),
          j = Object(C.g)(
            function () {
              return (
                Object(f.a)(this, e),
                null != h
                  ? h
                  : [{ urls: t.urls, width: t.width, height: t.height }]
              );
            }.bind(this),
            [h, t.height, t.urls, t.width]
          );
        return Object(i.jsx)(mt.a, {
          workType: l.q.Illust,
          id: n,
          children: function (s, h) {
            return (
              Object(f.a)(this, e),
              Object(i.jsxs)(Gn, {
                figure: Object(i.jsx)(i.Fragment, {
                  children:
                    void 0 !== h
                      ? h
                      : t.illustType === l.d.Ugoira
                      ? Object(i.jsx)(Ie, {
                          title: d,
                          idOrSecret: t.idOrSecret,
                          placeholderUrls: t.urls,
                          width: t.width,
                          height: t.height,
                        })
                      : Object(i.jsx)(En, {
                          workId: n,
                          workTitle: d,
                          workAlt: r,
                          workType: t.illustType,
                          pageCount: o,
                          pageIllusts: j,
                          onClickExpandToggle: t.onExpandClick,
                        }),
                }),
                sticky: Object(i.jsxs)(i.Fragment, {
                  children: [
                    Object(i.jsx)("div", { ref: u }),
                    s !== ft.a.UserAge &&
                      Object(i.jsx)(
                        Et.b,
                        {
                          pageType: b ? l.j.IllustUnlisted : l.j.Illust,
                          id: t.id,
                          blur: void 0 !== s,
                          onExpandClick: t.onExpandClick,
                          stickyActive: !0,
                          onlyOnePage: 1 === t.pageCount,
                        },
                        t.id
                      ),
                  ],
                }),
                children: [
                  a && Object(i.jsx)($t, { id: t.id }),
                  Object(i.jsx)(It, { workType: l.q.Illust, id: t.id }),
                  !1 === c &&
                    Object(i.jsx)(Ut.a, {
                      workType: l.q.Illust,
                      id: t.id,
                      isUnlisted: t.isUnlisted,
                      tagsEditable: t.tagsEditable,
                      onTagEditClick: t.onTagEditClick,
                    }),
                ],
              })
            );
          }.bind(this),
        });
      }
      function Gn(t) {
        var e = t.figure,
          n = t.sticky,
          r = t.children;
        return Object(i.jsxs)(Yn, {
          children: [
            Object(i.jsx)(Xn, { children: e }),
            n,
            o.a.Children.count(r) > 0 && Object(i.jsx)($n, { children: r }),
          ],
        });
      }
      var Zn = y.default.div.withConfig({ componentId: "sc-1yvhotl-1" })([
          "margin:auto;width:500px;",
        ]),
        Yn = y.default.div.withConfig({ componentId: "sc-1yvhotl-2" })([
          "position:relative;z-index:0;",
        ]),
        Xn = y.default.figure.withConfig({ componentId: "sc-1yvhotl-3" })(
          ["margin:0;background:", ";"],
          function (t) {
            return Object(f.a)(this, void 0), t.theme.color.surface7;
          }.bind(void 0)
        ),
        $n = y.default.figcaption.withConfig({ componentId: "sc-1yvhotl-4" })([
          "position:relative;overflow:hidden;",
        ]),
        Kn = y.default.div.withConfig({ componentId: "sc-1yvhotl-5" })([
          "margin:24px auto;padding:0 16px;max-width:568px;",
        ]),
        Qn = y.default.section.withConfig({ componentId: "sc-1yvhotl-6" })(
          ["border-top:1px solid ", ";margin:24px auto 64px;padding:0 16px;"],
          function (t) {
            return Object(f.a)(this, void 0), t.theme.color.border;
          }.bind(void 0)
        );
      function Jn(t) {
        var e = this,
          n = t.idOrSecret,
          r = Object(s.c)(
            function (t) {
              return Object(f.a)(this, e), T.d(t, l.q.Illust, n);
            }.bind(this)
          ),
          c = Object(s.c)(u.k),
          a = Object(s.c)(u.s),
          d = Object(s.b)(),
          b = Object(C.j)(!1),
          j = Object(h.a)(b, 2),
          p = j[0],
          O = j[1],
          m = Object(C.i)(void 0),
          v = Object(it.g)(),
          g = Object(nt.c)(),
          x = Object(C.a)(
            function () {
              if ((Object(f.a)(this, e), void 0 !== r)) {
                var t = v.pathname + v.search,
                  n = r.illustType,
                  i = r.xRestrict,
                  o = r.pageCount,
                  c =
                    i === l.r.Safe
                      ? "general"
                      : i === l.r.R18
                      ? "r18"
                      : i === l.r.R18G
                      ? "r18g"
                      : Object(k.v)(i),
                  a = o > 1 ? "true" : "false",
                  s =
                    n === l.d.Illust
                      ? "illust"
                      : n === l.d.Manga
                      ? "manga"
                      : n === l.d.Ugoira
                      ? "ugoira"
                      : Object(k.v)(n);
                _gaq.push([
                  "_setCustomVar",
                  21,
                  "illust_detail_illust_x_restrict",
                  c,
                ]),
                  _gaq.push(["_setCustomVar", 22, "illust_detail_is_multi", a]),
                  _gaq.push([
                    "_setCustomVar",
                    23,
                    "illust_detail_illust_type",
                    s,
                  ]),
                  _gaq.push(["_trackPageview", encodeURI(t)]),
                  _gaq.push(["_deleteCustomVar", 21]),
                  _gaq.push(["_deleteCustomVar", 22]),
                  _gaq.push(["_deleteCustomVar", 23]);
              }
            }.bind(this),
            [r, v]
          ),
          y = Object(C.a)(
            (function () {
              var t = Object(et.a)(
                tt.a.mark(function t(e) {
                  return tt.a.wrap(
                    function (t) {
                      for (;;)
                        switch ((t.prev = t.next)) {
                          case 0:
                            return (
                              (t.prev = 0), (t.next = 3), d(T.g(l.q.Illust, e))
                            );
                          case 3:
                            t.next = 8;
                            break;
                          case 5:
                            (t.prev = 5),
                              (t.t0 = t.catch(0)),
                              t.t0 instanceof L.a && t.t0.apiError
                                ? g(t.t0.apiError.message)
                                : g(t.t0.toString());
                          case 8:
                          case "end":
                            return t.stop();
                        }
                    },
                    t,
                    null,
                    [[0, 5]]
                  );
                })
              );
              return function (e) {
                return t.apply(this, arguments);
              };
            })(),
            [d, g]
          ),
          w = Object(C.a)(
            function () {
              Object(f.a)(this, e), O(!0);
            }.bind(this),
            [O]
          ),
          I = Object(C.a)(
            function () {
              Object(f.a)(this, e), O(!1);
            }.bind(this),
            [O]
          ),
          _ = Object(C.a)(
            function (t) {
              Object(f.a)(this, e),
                null === c ||
                  void 0 === r ||
                  r.userId === c ||
                  a ||
                  Object(ot.a)(c, t);
            }.bind(this),
            [c, r, a]
          );
        Object(C.d)(
          function () {
            var t = this;
            (Object(f.a)(this, e), void 0 !== r) &&
              ((void 0 === m.current || m.current.id !== r.id) &&
                (d(Object(ct.j)(1 === r.pageCount)),
                d(Object(ct.k)(!1)),
                requestAnimationFrame(
                  function () {
                    Object(f.a)(this, t), window.scroll({ top: 0 });
                  }.bind(this)
                ),
                Object(T.i)(n) || _(n.id),
                x()),
              (m.current = r));
          }.bind(this),
          [n, r, v, x, _, d]
        ),
          Object(C.d)(
            function () {
              Object(f.a)(this, e), void 0 === r && y(n);
            }.bind(this),
            [n, r, y]
          );
        var S = r || m.current;
        return void 0 === S
          ? Object(i.jsx)(Z.c, {})
          : Object(i.jsxs)(rt.b, {
              workType: l.q.Illust,
              id: S.id,
              children: [
                Object(i.jsx)(o.a.Suspense, {
                  fallback: null,
                  children: Object(i.jsx)(Fn, {
                    idOrSecret: n,
                    illustId: S.id,
                    tagsEditable: !0,
                    onTagEditClick: w,
                  }),
                }),
                !Object(T.i)(n) &&
                  Object(i.jsx)(at.a, {
                    show: p,
                    workType: l.q.Illust,
                    id: n.id,
                    onClose: I,
                  }),
              ],
            });
      }
      function ti(t) {
        var e = t.idOrSecret,
          n = !Object(s.c)(u.r);
        return Object(i.jsxs)(i.Fragment, {
          children: [
            Object(i.jsxs)(a.g, {
              children: [
                Object(i.jsx)(a.t, {
                  children: Object(i.jsx)(a.v, {
                    children: Object(i.jsx)(Jn, { idOrSecret: e }),
                  }),
                }),
                Object(i.jsx)(a.d, {
                  children: Object(i.jsx)(K, { idOrSecret: e }),
                }),
              ],
            }),
            Object(i.jsx)(ei, {
              children: Object(i.jsx)(c.c, { kind: c.a.Footer }),
            }),
            !Object(T.i)(e) &&
              Object(i.jsxs)(i.Fragment, {
                children: [
                  Object(i.jsx)("aside", {
                    children: Object(i.jsx)(d.a, {
                      workType: l.q.Illust,
                      id: e.id,
                    }),
                  }),
                  Object(i.jsx)("aside", {
                    children: Object(i.jsx)(o.a.Suspense, {
                      fallback: null,
                      children: Object(i.jsx)(b.a, {
                        workType: l.q.Illust,
                        id: e.id,
                        viewMore: n,
                      }),
                    }),
                  }),
                  Object(i.jsx)("aside", {
                    children: Object(i.jsx)(_, {
                      workType: l.q.Illust,
                      id: e.id,
                    }),
                  }),
                ],
              }),
          ],
        });
      }
      var ei = Object(y.default)(a.c).withConfig({ componentId: "zfvkpm-0" })([
        "padding-bottom:56px;",
      ]);
    },
    654: function (t, e, n) {
      t.exports = { userIcon: "_2fj9Unu", coverTexture: "_3HZmrVs" };
    },
    662: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return m;
      }),
        n.d(e, "d", function () {
          return v;
        }),
        n.d(e, "h", function () {
          return g;
        }),
        n.d(e, "j", function () {
          return x;
        }),
        n.d(e, "k", function () {
          return y;
        }),
        n.d(e, "b", function () {
          return w;
        }),
        n.d(e, "e", function () {
          return k;
        }),
        n.d(e, "i", function () {
          return I;
        }),
        n.d(e, "g", function () {
          return _;
        }),
        n.d(e, "c", function () {
          return S;
        }),
        n.d(e, "f", function () {
          return P;
        });
      n(52), n(96), n(31), n(129), n(65);
      var i = n(8),
        r = n(1),
        o = n(1088),
        c = n(61),
        a = n(101),
        s = n(6),
        l = n(20),
        u = n(90),
        d = n(875),
        b = n(42),
        h = n(15),
        f = n(59),
        j = n(3),
        p = n(64),
        O = n(5);
      function m(t) {
        var e = Object(b.m)(j.j.Commission);
        return null !== e && e.internalPageType === t ? e : null;
      }
      function v(t) {
        var e = this;
        return Object(h.c)(
          function (n) {
            return (
              Object(r.a)(this, e), t ? n.pageCommission.requests[t] : void 0
            );
          }.bind(this)
        );
      }
      function g(t) {
        var e = this;
        return Object(h.c)(
          function (n) {
            var i = this;
            return (
              Object(r.a)(this, e),
              t.reduce(
                function (t, e) {
                  return (
                    Object(r.a)(this, i),
                    n.pageCommission.requests[e] &&
                      t.push(n.pageCommission.requests[e]),
                    t
                  );
                }.bind(this),
                []
              )
            );
          }.bind(this),
          h.a
        );
      }
      function x(t) {
        var e = this;
        return Object(h.c)(
          function (n) {
            var i = this;
            return (
              Object(r.a)(this, e),
              t.reduce(
                function (t, e) {
                  return (
                    Object(r.a)(this, i),
                    n.pageCommission.userList[e] &&
                      t.push(n.pageCommission.userList[e]),
                    t
                  );
                }.bind(this),
                []
              )
            );
          }.bind(this),
          h.a
        );
      }
      function y(t) {
        var e = this;
        return Object(h.c)(
          function (n) {
            var i = this;
            return (
              Object(r.a)(this, e),
              t.reduce(
                function (t, e) {
                  return (
                    Object(r.a)(this, i),
                    n.pageCommission.userListDesktop[e] &&
                      t.push(n.pageCommission.userListDesktop[e]),
                    t
                  );
                }.bind(this),
                []
              )
            );
          }.bind(this),
          h.a
        );
      }
      function w(t, e) {
        var n = this,
          i = e.excludeHidden || !0,
          o = e.excludeNoWork || !1,
          a = e.unique || !1,
          l = g(t),
          u = C(l),
          d = (function (t) {
            var e = this,
              n = g(t)
                .map(
                  function (t) {
                    if ((Object(r.a)(this, e), t.postWork))
                      return {
                        workId: t.postWork.postWorkId,
                        workType:
                          "novel" === t.postWork.postWorkType
                            ? j.q.Novel
                            : j.q.Illust,
                        requestId: t.requestId,
                      };
                  }.bind(this)
                )
                .filter(c.b.isPresent),
              i = n
                .filter(
                  function (t) {
                    return Object(r.a)(this, e), t.workType === j.q.Illust;
                  }.bind(this)
                )
                .map(
                  function (t) {
                    return Object(r.a)(this, e), t.workId;
                  }.bind(this)
                ),
              o = n
                .filter(
                  function (t) {
                    return Object(r.a)(this, e), t.workType === j.q.Novel;
                  }.bind(this)
                )
                .map(
                  function (t) {
                    return Object(r.a)(this, e), t.workId;
                  }.bind(this)
                ),
              a = Object(f.l)(j.q.Illust, i, { excludeMuted: !0 }).map(
                function (t) {
                  return Object(r.a)(this, e), t.id;
                }.bind(this)
              ),
              s = Object(f.l)(j.q.Novel, o, { excludeMuted: !0 }).map(
                function (t) {
                  return Object(r.a)(this, e), t.id;
                }.bind(this)
              );
            return t.reduce(
              function (t, i) {
                var o = this;
                Object(r.a)(this, e);
                var c = n.find(
                  function (t) {
                    return Object(r.a)(this, o), t.requestId === i;
                  }.bind(this)
                );
                return (
                  (t[i] =
                    !!c &&
                    (c.workType === j.q.Illust
                      ? !!a.find(
                          function (t) {
                            return Object(r.a)(this, o), t === c.workId;
                          }.bind(this)
                        )
                      : !!s.find(
                          function (t) {
                            return Object(r.a)(this, o), t === c.workId;
                          }.bind(this)
                        ))),
                  t
                );
              }.bind(this),
              {}
            );
          })(t);
        return Object(s.useMemo)(
          function () {
            var t = this;
            Object(r.a)(this, n);
            var e = l.filter(
              function (e) {
                return (
                  Object(r.a)(this, t),
                  (!i || !1 === u[e.requestId]) &&
                    !(
                      o &&
                      (!d[e.requestId] ||
                        (e.postWork &&
                          e.postWork.work &&
                          e.postWork.work.isUnlisted))
                    )
                );
              }.bind(this)
            );
            return a ? c.a.unique(e) : e;
          }.bind(this),
          [l, u, d, i, o, a]
        );
      }
      function k(t, e) {
        var n = C([t])[0];
        return ("mute" !== n || !e) && n;
      }
      function C(t) {
        var e = this,
          n = Object(p.h)(),
          i = Object(h.c)(
            function (i) {
              var o = this;
              return (
                Object(r.a)(this, e),
                t.reduce(
                  function (t, e) {
                    return (
                      Object(r.a)(this, o),
                      (t[e.requestId] = Object(a.i)(i, {
                        userId: (null == n ? void 0 : n.id) || "1",
                        tags: e.requestTags,
                      })),
                      t
                    );
                  }.bind(this),
                  {}
                )
              );
            }.bind(this),
            h.a
          ),
          o = Object(h.c)(
            function (n) {
              var i = this;
              return (
                Object(r.a)(this, e),
                t.reduce(
                  function (t, e) {
                    return (
                      Object(r.a)(this, i),
                      (t[e.requestId] = Object(a.i)(n, {
                        userId: e.fanUserId || "1",
                        tags: [],
                      })),
                      t
                    );
                  }.bind(this),
                  {}
                )
              );
            }.bind(this),
            h.a
          ),
          c = Object(h.c)(
            function (n) {
              var i = this;
              return (
                Object(r.a)(this, e),
                t.reduce(
                  function (t, e) {
                    return (
                      Object(r.a)(this, i),
                      (t[e.requestId] = Object(a.i)(n, {
                        userId: e.creatorUserId,
                        tags: [],
                      })),
                      t
                    );
                  }.bind(this),
                  {}
                )
              );
            }.bind(this),
            h.a
          );
        return Object(s.useMemo)(
          function () {
            var a = this;
            return (
              Object(r.a)(this, e),
              t.reduce(
                function (t, e) {
                  return (
                    Object(r.a)(this, a),
                    !e.requestAdultFlg || (n && n.xRestrict !== l.k.Safe)
                      ? i[e.requestId] || o[e.requestId] || c[e.requestId]
                        ? ((t[e.requestId] = "mute"), t)
                        : ((t[e.requestId] = !1), t)
                      : ((t[e.requestId] = "adult"), t)
                  );
                }.bind(this),
                {}
              )
            );
          }.bind(this),
          [t, n, i, o, c]
        );
      }
      function I() {
        var t = this,
          e = Object(u.a)(),
          n = Object(s.useState)(!e),
          o = Object(i.a)(n, 2),
          c = o[0],
          a = o[1],
          l = Object(s.useCallback)(
            function () {
              var n = this;
              Object(r.a)(this, t),
                e &&
                  a(
                    function (t) {
                      return Object(r.a)(this, n), !t;
                    }.bind(this)
                  );
            }.bind(this),
            [a, e]
          );
        return (
          Object(s.useEffect)(
            function () {
              Object(r.a)(this, t), e || c || a(!0);
            }.bind(this),
            [e, c]
          ),
          [c, l]
        );
      }
      function _(t) {
        var e = this,
          n = arguments.length > 1 && void 0 !== arguments[1] && arguments[1],
          i = Object(O.n)(),
          o = Object(s.useMemo)(
            function () {
              var o = this;
              if (
                (Object(r.a)(this, e),
                Object(d.a)(t.requestProposal.requestOriginalProposalLang) ===
                  i)
              )
                return n
                  ? t.requestProposal.requestOriginalProposalHtml
                  : t.requestProposal.requestOriginalProposal;
              var c = "ja" === i ? i : "en",
                a = t.requestProposal.requestTranslationProposal.find(
                  function (t) {
                    return Object(r.a)(this, o), t.requestProposalLang === c;
                  }.bind(this)
                );
              return a && n
                ? a.requestProposalHtml
                : a
                ? a.requestProposal
                : void 0;
            }.bind(this),
            [
              t.requestProposal.requestOriginalProposal,
              t.requestProposal.requestOriginalProposalHtml,
              t.requestProposal.requestOriginalProposalLang,
              t.requestProposal.requestTranslationProposal,
              i,
              n,
            ]
          );
        return o;
      }
      function S(t) {
        var e = this,
          n = arguments.length > 1 && void 0 !== arguments[1] && arguments[1],
          i = Object(O.n)();
        return Object(s.useMemo)(
          function () {
            if (
              (Object(r.a)(this, e),
              Object(d.a)(t.planDescription.planOriginalLang) === i)
            )
              return n
                ? t.planDescription.planOriginalDescriptionHtml
                : t.planDescription.planOriginalDescription;
            var o = "ja" === i ? i : "en",
              c = t.planDescription.planTranslationDescription[o];
            return void 0 !== c
              ? n
                ? c.planDescriptionHtml
                : c.planDescription
              : void 0;
          }.bind(this),
          [i, t.planDescription, n]
        );
      }
      function P(t, e) {
        var n,
          c,
          a,
          s,
          l = this,
          u = Object(O.p)([O.m.commission.ns]),
          d = Object(i.a)(u, 1)[0],
          b = Object(p.h)(),
          h = 0 === t.collaborateStatus.collaboratedCnt,
          f = t.collaborateStatus.collaborating,
          j = t.collaborateStatus.collaboratedCnt,
          m = "fanUserId" in t ? t.role : t.request.role,
          v = "fanUserId" in t ? t.requestAnonymousFlg : null === t.fan,
          g = Object(p.j)(
            "fanUserId" in t && null !== (n = t.fanUserId) && void 0 !== n
              ? n
              : void 0
          ),
          x =
            m === o.REQUEST_USER_ROLE_FAN
              ? d(O.m.commission.user.you)
              : d(O.m.commission.user.decorate_san, {
                  name: v
                    ? d(O.m.commission.user.anonymous)
                    : "fanUserId" in t
                    ? null == g
                      ? void 0
                      : g.name
                    : null === (c = t.fan) || void 0 === c
                    ? void 0
                    : c.userName,
                }),
          y =
            "fanUserId" in t
              ? t.collaborateStatus.collaborateUserSamples.filter(
                  function (t) {
                    return (
                      Object(r.a)(this, l),
                      t.collaborateUserId !== (null == b ? void 0 : b.id)
                    );
                  }.bind(this)
                )
              : [],
          w = Object(p.j)(
            "fanUserId" in t &&
              null !==
                (a =
                  null === (s = y[0]) || void 0 === s
                    ? void 0
                    : s.collaborateUserId) &&
              void 0 !== a
              ? a
              : void 0
          ),
          k = "";
        if ("fanUserId" in t && y.length > 0) {
          var C = y[0].collaborateAnonymousFlg;
          k = d(
            O.m.commission.user.decorate_san,
            C
              ? { name: d(O.m.commission.user.anonymous) }
              : { name: null == w ? void 0 : w.name }
          );
        }
        if (!("fanUserId" in t) && t.collaborateStatus.userSamples.length > 0) {
          var I,
            _ = t.collaborateStatus.userSamples.filter(
              function (t) {
                return (
                  Object(r.a)(this, l),
                  (null == t ? void 0 : t.userId) !==
                    (null == b ? void 0 : b.id)
                );
              }.bind(this)
            ),
            S = null === _[0];
          k = d(
            O.m.commission.user.decorate_san,
            S
              ? { name: d(O.m.commission.user.anonymous) }
              : {
                  name:
                    null === (I = _[0]) || void 0 === I ? void 0 : I.userName,
                }
          );
        }
        return h
          ? d(e.fan_only, { fanName: x })
          : f
          ? 1 === j
            ? d(e.collaborating_collaborate_one, { fanName: x })
            : 2 === j
            ? d(e.collaborating_collaborate_two, {
                fanName: x,
                collaborateUserName: k,
              })
            : d(e.collaborating_collaborate_many, { fanName: x })
          : j > 1
          ? d(e.collaborate_many, { fanName: x })
          : d(e.collaborate_one, { fanName: x, collaborateUserName: k });
      }
    },
    720: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return o;
      });
      var i = n(0),
        r = (n(6), n(43));
      function o() {
        var t = Object(i.jsx)(i.Fragment, {
          children: Object(i.jsx)("path", {
            d:
              "m8.2896 20c7.5472 0 11.6752-6.1562 11.6752-11.49475 0-.17486 0-.34892-.012-.5222.8031-.5719 1.4963-1.28 2.0472-2.09117-.7489.32671-1.5433.54097-2.3568.63562.8566-.50489 1.4977-1.29899 1.804-2.23452-.8055.47057-1.6867.80221-2.6056.9806-1.2719-1.33158-3.293-1.65749-4.93-.79497-1.6369.86251-2.4826 2.69894-2.0628 4.47953-3.29928-.16284-6.37322-1.6971-8.4568-4.22094-1.0891 1.84594-.53281 4.20744 1.2704 5.3929-.65301-.019-1.29178-.19245-1.8624-.50562v.0512c.00053 1.92312 1.3774 3.57942 3.292 3.96022-.6041.1622-1.23794.1859-1.8528.0693.53756 1.6457 2.07806 2.7731 3.8336 2.8056-1.45301 1.1243-3.24795 1.7346-5.096 1.7328-.32648-.0006-.65264-.0201-.9768-.0583 1.87651 1.1856 4.05993 1.8145 6.2896 1.8115",
            fill: "#1da1f2",
          }),
        });
        return Object(i.jsx)(r.b, {
          viewBoxSize: 24,
          size: 24,
          currentColor: !0,
          path: t,
          transform: "",
        });
      }
    },
    721: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return o;
      });
      var i = n(0),
        r = (n(6), n(43));
      function o() {
        var t = Object(i.jsxs)(i.Fragment, {
          children: [
            Object(i.jsx)("path", {
              d:
                "m22 12c0-5.52286-4.4771-10.00001-10-10.00001-5.52285 0-10 4.47715-10 10.00001 0 4.9913 3.65686 9.1283 8.4375 9.8785v-6.9879h-2.53906v-2.8906h2.53906v-2.20314c0-2.50625 1.4929-3.89062 3.7771-3.89062 1.0941 0 2.2385.19531 2.2385.19531v2.46094h-1.261c-1.2422 0-1.6296.77084-1.6296 1.56171v1.8758h2.7734l-.4433 2.8906h-2.3301v6.9879c4.7806-.7502 8.4375-4.8872 8.4375-9.8785z",
              fill: "#1877f2",
            }),
            Object(i.jsx)("path", {
              d:
                "m15.8926 14.8906.4433-2.8906h-2.7734v-1.8758c0-.79086.3874-1.5617 1.6296-1.5617h1.261v-2.46094s-1.1444-.19531-2.2385-.19531c-2.2842 0-3.7771 1.38437-3.7771 3.89062v2.20313h-2.53906v2.8906h2.53906v6.9879c.5091.0799 1.0309.1215 1.5625.1215s1.0534-.0416 1.5625-.1215v-6.9879z",
              fill: "#fff",
            }),
          ],
        });
        return Object(i.jsx)(r.b, {
          viewBoxSize: 24,
          size: 24,
          currentColor: !0,
          path: t,
          transform: "",
        });
      }
    },
    724: function (t, e, n) {
      "use strict";
      n.d(e, "a", function () {
        return s;
      });
      var i = n(0),
        r = n(55),
        o = n.n(r),
        c = (n(6), n(654)),
        a = n(11);
      function s(t) {
        var e = t.url,
          n = t.size;
        return Object(i.jsx)("div", {
          className: o()(c.userIcon, c.coverTexture),
          style: { backgroundImage: Object(a.d)(e), width: n, height: n },
        });
      }
      s.defaultProps = { size: 40 };
    },
    875: function (t, e, n) {
      "use strict";
      n.d(e, "e", function () {
        return a;
      }),
        n.d(e, "c", function () {
          return s;
        }),
        n.d(e, "b", function () {
          return l;
        }),
        n.d(e, "a", function () {
          return u;
        }),
        n.d(e, "d", function () {
          return d;
        });
      n(94), n(109);
      var i = n(61),
        r = n(40),
        o = n(119),
        c = n(5);
      function a(t) {
        switch (t.requestStatus) {
          case "waitingResponse":
            return o.u;
          case "inProgress":
            return o.q;
          case "inReview":
          case "waitingFix":
            return o.r;
          case "complete":
            return o.p;
          case "refuse":
          case "responseTimeout":
          case "postTimeout":
            var e;
            if (
              (null === (e = t.requestResend) || void 0 === e
                ? void 0
                : e.requestResendStatus) === o.d
            )
              switch (t.role) {
                case "creator":
                  return o.t;
                case "fan":
                  return o.s;
              }
            return o.o;
          case "reject":
          case "sendCancel":
          case "stop":
            return o.o;
          default:
            i.b.unreachable(t);
        }
      }
      function s(t, e) {
        var n = Object(r.default)(),
          i = e.diff(n, "days"),
          o = e.diff(n, "hours");
        if (o >= 24)
          return t(
            c.m.commission.manage_request_list_item.deadline_days_str_days,
            { days: i }
          );
        if (o > 0)
          return t(
            c.m.commission.manage_request_list_item.deadline_days_str_hours,
            { hours: o }
          );
        var a = e.diff(n, "minutes");
        return (
          a < 0 && (a = 0),
          t(c.m.commission.manage_request_list_item.deadline_days_str_minutes, {
            minutes: a,
          })
        );
      }
      function l(t) {
        var e = Object(r.default)(),
          n = t.diff(e, "days"),
          i = ["en", "ko"].includes(t.locale()) ? "MMM Do" : "MMMDo";
        return n <= 3 ? t.format("".concat(i, " H:mm")) : t.format(i);
      }
      function u(t) {
        switch (t) {
          case "":
            return "";
          case "_unsup":
            return "_unsup";
          default:
            return Object(c.j)(t);
        }
      }
      function d(t) {
        var e;
        return (
          "creator" === t.role &&
          !!t.requestResend &&
          (!!t.requestResend.requestResendOfferEnabled ||
            [o.b, o.c].includes(
              null !== (e = t.requestResend.requestResendStatus) && void 0 !== e
                ? e
                : ""
            ))
        );
      }
    },
  },
]);
