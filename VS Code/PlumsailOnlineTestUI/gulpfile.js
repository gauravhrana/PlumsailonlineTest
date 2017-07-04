var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var htmlmin = require('gulp-htmlmin');
var cleanCSS = require('gulp-clean-css');

gulp.task('copyHtml', function () {
    gulp.src('index.html')
        .pipe(gulp.dest('dist'));

    gulp.src('app/modules/home/views/home.html')
        .pipe(gulp.dest('dist/app/modules/home/views/'));

    gulp.src('app/modules/save/views/save.html')
        .pipe(gulp.dest('dist/app/modules/save/views/'));

    gulp.src('fonts/*.*')
        .pipe(gulp.dest('dist/fonts/'));
});

// minify all external js and put in dist/js folder
gulp.task('vendorJs', function () {
    gulp.src('js/*.min.js')
        .pipe(gulp.dest('dist/js'));

    gulp.src('js/ui-bootstrap-tpls-2.5.0.js')
        .pipe(concat('ui-bootstrap-tpls-2.5.0.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('dist/js'));
});

// minify all app js and put in dist/js folder
gulp.task('appJs', function () {
    gulp.src(['app/app.js'
        , 'app/app.config.js'
        , 'app/modules/data/js/*.js'
        , 'app/modules/home/js/*.js'
        , 'app/modules/save/js/*.js'])
        .pipe(concat('app.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('dist/js'));
});

gulp.task('minifyCss', () => {
    return gulp.src('css/*.css')
        .pipe(cleanCSS({ compatibility: 'ie8' }))
        .pipe(concat('site.min.css'))
        .pipe(gulp.dest('dist/css'));
});

// Watch for changes in files
gulp.task('watch', function () {

    // Watch .js files
    gulp.watch(['app/app.js'
        , 'app/app.config.js'
        , 'app/modules/data/js/*.js'
        , 'app/modules/home/js/*.js'
        , 'app/modules/save/js/*.js'], ['appJs']);

    // Watch .css files
    gulp.watch('css/*.css', ['minifyCss']);
});

gulp.task('default', ['copyHtml', 'vendorJs', 'appJs', 'minifyCss']);