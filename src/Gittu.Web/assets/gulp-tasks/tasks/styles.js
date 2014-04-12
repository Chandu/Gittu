var gulp = require('gulp');
var	less = require('gulp-less');
var clean = require('gulp-clean');

module.exports = function() {

	gulp.task('styles', function () {
		gulp.src('./css/*.css')
			.pipe(gulp.dest('../public/css/'));
		gulp.src('./css/gittu.less')
			.pipe(less())
			.pipe(gulp.dest('../public/css/'));
	});
};