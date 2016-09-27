
var gulp = require('gulp'),
    uglify = require('gulp-uglify'),
    sass = require('gulp-sass'),
    polyfill = require('babel-polyfill'),
    babel = require("gulp-babel"),
    concat = require('gulp-concat');
    

gulp.task('sass', function () {
  return gulp.src('../Website/Content/SCSS/main.scss')
    .pipe(sass().on('error', sass.logError))
    .pipe(gulp.dest('../Website/Content/css'));
});
gulp.task("babel", function () {
  return gulp.src("../Website/Content/JS/layout/**")                        
      .pipe(babel())
    .pipe(gulp.dest("../Website/Content/JS/babel/"));
});
gulp.task('watch', function() {
gulp.watch('../Website/Content/SCSS/**', ['sass']);
});

gulp.task('default', ['watch']);