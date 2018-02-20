var gulp = require('gulp');

gulp.task("frontend:create-lib", function (cb) {
    var npm = {
        "signalr": "@aspnet/signalr-client/dist/browser/*.js"
    }
    for (var package in npm) {
        gulp.src("./node_modules/" + npm[package])
            .pipe(gulp.dest("./wwwroot/lib/" + package));
    }
    cb();
});