var gulp = require('gulp'),
    uglify = require('gulp-uglify'),
    sass = require('gulp-sass'),
    concat = require('gulp-concat');

gulp.task('sass', function () {
  return gulp.src('../Website/Content/SCSS/main.scss')
    .pipe(sass().on('error', sass.logError))
    .pipe(gulp.dest('../Website/Content/css'));
});

gulp.task('watch', function() {
gulp.watch('../Website/Content/SCSS/**', ['sass']);
gulp.watch(['../Website/Content/js/**','!../Website/Content/js/plugin' ], ['script']);
});

gulp.task('default', ['watch']);