var gulp = require('gulp')
	less = require('gulp-less'),
	path = require('path'),
	watch = require('gulp-watch');

var resouces = {
	"scripts" :  [
		"./assets/components/modernizr/modernizr.js",
		"./assets/components/jquery/jquery.js",
		"./assets/components/bootstrap/dist/bootstrap.js"
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
	gulp.src(resouces.styles)
		.pipe(less())
		.pipe(gulp.dest('./public/css/'));

	gulp.src(resouces.images)
		.pipe(gulp.dest('./public/img/'));
});

// task to run while actively developing
gulp.task('watch', ['scripts','styles', 'images'], function () {
    gulp.watch(resouces.scripts, ['scripts']);
    gulp.watch(resouces.styles, ['styles']);
    gulp.watch(resouces.images, ['images']);
});