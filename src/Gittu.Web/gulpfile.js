var gulp = require('gulp');
var	less = require('gulp-less');
var	path = require('path');
var	watch = require('gulp-watch');
var sys = require('sys');
var exec = require('child_process').exec;

var resouces = {
	"scripts" :  [
		"./assets/components/modernizr/modernizr.js",
		"./assets/components/jquery/jquery.js",
		"./assets/components/bootstrap/dist/js/bootstrap.js",
		"./assets/components/json3/lib/json3.js",
		"./assets/js/*.js"
	],
	"styles" : [
		"./assets/css/gittu.less"
	],
	"images" : [
		"./assets/img/*.*"
	]
};

gulp.task('default', ['images', 'styles', 'scripts'], function() {
	console.log("Default task done.");
});

gulp.task('scripts',  function () {
	gulp.src(resouces.scripts)
		.pipe(gulp.dest('./public/js/'));
});

gulp.task('styles', function () {
	gulp.src('./assets/css/*.css')
		.pipe(gulp.dest('./public/css/'));
	gulp.src(resouces.styles)
		.pipe(watch(function(files) {
			return files.pipe(less())
				.pipe(gulp.dest('./public/css/'));
		}));
});

gulp.task('images', function () {
	gulp.src(resouces.images)
		.pipe(gulp.dest('./public/img/'));
});

gulp.task('migrate', function () {
	sys.puts('for now use rake');
});

// task to run while actively developing
gulp.task('watch', ['scripts','styles', 'images'], function () {
    gulp.watch(resouces.scripts, ['scripts']);
    gulp.watch(resouces.styles, ['styles']);
    gulp.watch(resouces.images, ['images']);
});