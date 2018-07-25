var gulp = require("gulp");
var rimraf = require("rimraf");
var fs = require("fs");
var gulpif = require("gulp-if");
var concat = require("gulp-concat");
var rename = require("gulp-rename");
var uglify = require("gulp-uglify");
var del = require("del");
var gzip = require("gulp-gzip");
var del = require("del");
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var cssmin = require('gulp-cssmin');
var rename = require('gulp-rename');
var runSequence = require('run-sequence');
var minifyCss = require("gulp-minify-css");
var minifyHtml = require("gulp-minify-html");
var browserify = require('browserify');
var ts = require('gulp-typescript');
var source = require('vinyl-source-stream');
var buffer = require('vinyl-buffer');
var babel = require('gulp-babel');

var paths = {
    dashboard: {
        js: {
            src: [
                "./Client/Dashboard/app/**/*.js"
            ],
            dest: "./wwwroot/dash/js/"
        },
        img: {
            faviconSrc: [
                "./Client/Dashboard/img/favicon/*.*"
            ],
            faviconDest: "./wwwroot/",
            src: [
                "./Client/Dashboard/img/**/*.*"
            ],
            dest: "./wwwroot/dash/img/"
        },
        css: {
            src: [
                "./Client/Dashboard/css/**/*.css"
            ],
            dest: "./wwwroot/dash/css/"
        },
        html: {
            src: [
                "./Client/Dashboard/app/**/*.html"
            ],
            dest: "./wwwroot/dash/html/"
        },
        fonts: {
        },
        vendors: {
            src: "./node_modules/",
            dest: "./wwwroot/dash/libs/"
        }
    },
    landing: {
        img: {
            src: [
                "./Client/Landing/img/**/*.*"
            ],
            dest: "./wwwroot/landing/img/"
        },
        css: {
            src: [
                "./Client/Landing/css/**/*.css"
            ],
            dest: "./wwwroot/landing/css/"
        },
        vendors: {
            src: "./node_modules/",
            dest: "./wwwroot/landing/libs/"
        }
    }
};

/*Typescript*/
var tsProject = ts.createProject('tsconfig.json');
var clientOutDir = tsProject.options.outDir;

gulp.task("dashboard:create-pixel-battles-js", function (cb) {
    runSequence(
        "dashboard:clean-typescript-temps",
        "dashboard:compile-typescript",
        "dashboard:generate-pixel-battles-package",
        "dashboard:clean-typescript-temps",
        cb
    );
});

gulp.task("dashboard:clean-typescript-temps", function (cb) {
    return del([clientOutDir]);
});

gulp.task("dashboard:compile-typescript", function (cb) {
    return tsProject.src()
        .pipe(tsProject())
        .pipe(gulp.dest(clientOutDir));
});

gulp.task("dashboard:generate-pixel-battles-package", function (cb) {
    return browserify(clientOutDir + '/PixelBattle.js', { standalone: 'pixelBattle' })
        .bundle()
        .pipe(source('pixelBattle.js'))
        .pipe(gulp.dest(paths.dashboard.js.dest))
        .pipe(buffer())
        .pipe(rename({ extname: '.min.js' }))
        .pipe(babel({ presets: ['minify'] }))
        .pipe(gulp.dest(paths.dashboard.js.dest))
        .pipe(gzip())
        .pipe(gulp.dest(paths.dashboard.js.dest));
});

/*JavaScript*/

gulp.task("dashboard:create-js", function (cb) {
    runSequence(
        "dashboard:clean-js",
        "dashboard:create-custom-js",
        "dashboard:create-pixel-battles-js",
        cb
    );
});

gulp.task("dashboard:create-custom-js", function (cb) {
    return gulp.src(paths.dashboard.js.src)
        .pipe(concat("app.js"))
        .pipe(gulp.dest(paths.dashboard.js.dest))
        .pipe(rename("app.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(paths.dashboard.js.dest))
        .pipe(gzip())
        .pipe(gulp.dest(paths.dashboard.js.dest));
});

gulp.task("dashboard:clean-js", function (cb) {
    rimraf(paths.dashboard.js.dest, cb);
});

/*Styles*/

gulp.task("dashboard:create-css", ["dashboard:clean-css"], function () {
    return gulp.src(paths.dashboard.css.src)
        .pipe(concat("style.css"))
        .pipe(gulp.dest(paths.dashboard.css.dest))
        .pipe(rename("style.min.css"))
        .pipe(minifyCss())
        .pipe(gulp.dest(paths.dashboard.css.dest))
        .pipe(gzip())
        .pipe(gulp.dest(paths.dashboard.css.dest));
});

gulp.task("dashboard:clean-css", function (cb) {
    rimraf(paths.dashboard.css.dest, cb);
});

gulp.task("landing:create-css", ["landing:clean-css"], function () {
    return gulp.src(paths.landing.css.src)
        .pipe(concat("style.css"))
        .pipe(gulp.dest(paths.landing.css.dest))
        .pipe(rename("style.min.css"))
        .pipe(minifyCss())
        .pipe(gulp.dest(paths.landing.css.dest))
        .pipe(gzip())
        .pipe(gulp.dest(paths.landing.css.dest));
});

gulp.task("landing:clean-css", function (cb) {
    rimraf(paths.landing.css.dest, cb);
});

/*Vendors*/

gulp.task("dashboard:create-vendors", ["dashboard:clean-vendors"], function (cb) {
    var libs = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,woff2,eot}",
        "jquery": "jquery/dist/jquery*.{js,map,css,ttf,svg,woff,eot}"
    };
    for (var lib in libs) {
        gulp.src(paths.dashboard.vendors.src + libs[lib])
            .pipe(gulp.dest(paths.dashboard.vendors.dest + lib))
            .pipe(gzip())
            .pipe(gulp.dest(paths.dashboard.vendors.dest + lib));
    }
    cb();
});

gulp.task("dashboard:clean-vendors", function (cb) {
    rimraf(paths.dashboard.vendors.dest, cb);
});

gulp.task("landing:create-vendors", ["landing:clean-vendors"], function (cb) {
    var libs = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,woff2,eot}",
        "jquery": "jquery/dist/jquery*.{js,map,css,ttf,svg,woff,eot}"
    };
    for (var lib in libs) {
        gulp.src(paths.landing.vendors.src + libs[lib])
            .pipe(gulp.dest(paths.landing.vendors.dest + lib))
            .pipe(gzip())
            .pipe(gulp.dest(paths.landing.vendors.dest + lib));
    }
    cb();
});

gulp.task("landing:clean-vendors", function (cb) {
    rimraf(paths.landing.vendors.dest, cb);
});

/*Images*/

gulp.task("dashboard:create-img", ["dashboard:clean-img"], function (cb) {
    return gulp.src(paths.dashboard.img.src)
        .pipe(gulp.dest(paths.dashboard.img.dest));
});

gulp.task("dashboard:clean-img", function (cb) {
    rimraf(paths.dashboard.img.dest, cb);
});

gulp.task("landing:create-img", ["landing:clean-img"], function (cb) {
    return gulp.src(paths.landing.img.src)
        .pipe(gulp.dest(paths.landing.img.dest));
});

gulp.task("landing:clean-img", function (cb) {
    rimraf(paths.landing.img.dest, cb);
});

gulp.task("dashboard:create-img-favicon", ["dashboard:clean-img-favicon"], function (cb) {
    return gulp.src(paths.dashboard.img.faviconSrc)
        .pipe(gulp.dest(paths.dashboard.img.faviconDest));
});

gulp.task("dashboard:clean-img-favicon", function (cb) {
    return del(paths.dashboard.img.faviconDest + "favicon.ico");
});

/*Global*/

gulp.task("dashboard:rebuild", function (cb) {
    runSequence(
        "dashboard:create-js",
        "dashboard:create-css",
        "dashboard:create-vendors",
        "dashboard:create-img",
        "dashboard:create-img-favicon",
        cb
    );
});

gulp.task("landing:rebuild", function (cb) {
    runSequence(
        "landing:create-css",
        "landing:create-vendors",
        "landing:create-img",
        cb
    );
});

gulp.task("global:rebuild", function (cb) {
    runSequence(
        "dashboard:rebuild",
        "landing:rebuild",
        cb
    );
});