
var gulp = require('gulp'),
    sass = require('gulp-sass'),
    minify = require('gulp-minify'),
    concat = require('gulp-concat');
    

gulp.task('sass', function () {
  return gulp.src('../Website/Content/SCSS/main.scss')
    .pipe(sass({outputStyle: 'compressed'}).on('error', sass.logError))
    .pipe(gulp.dest('../Website/Content/css'));
});

//gulp.task('concat', function() {
// return  gulp.src('../Website/Content/JS/layout/*.js')
//		 .pipe(concat('all.js'))
//		 .pipe(gulp.dest('../Website/Content/JS/layout/minified/'));
//});
 
//gulp.task('compress', function() {
// return gulp.src('../Website/Content/JS/layout/minified/all.js')
//    .pipe(minify({
//        ext:{
//            min:'-min.js'
//        }
//    }))
//    .pipe(gulp.dest('../Website/Content/JS/layout/minified/'));
//});

gulp.task('watch', function() {
gulp.watch('../Website/Content/SCSS/**', ['sass']);
//gulp.watch('../Website/Content/JS/layout/**', ['concat','compress']);

});

gulp.task('default', ['watch']);