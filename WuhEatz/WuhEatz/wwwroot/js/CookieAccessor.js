window.CookieAccessor = {
  get: function () {
    return document.cookie;
  },
  set: function (cookies) {
    document.cookie = cookies;
  }
}