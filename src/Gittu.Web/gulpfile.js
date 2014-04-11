var gulp = require('gulp');
var	less = require('gulp-less');
var	watch = require('gulp-watch');
var uglify = require('uglify-js');
var gulpif= require('gulp-if');
var browserify= require('browserify');
var glob = require("glob");
var path = require("path");
var source = require("vinyl-source-stream");

var resources = {
	"scripts" :  {
		"common" : ["./assets/components/modernizr/modernizr.js"],
		"apps" : "./app/*.app.js" 
	},
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

gulp.task('browserify',  function () {
	glob(resources.scripts.apps, null, function (er, files) {
		files.forEach(function(file) {
			browserify({
				debug: true
			}).require(file, {entry: true})
			.bundle()
			.pipe(source(path.basename(file)))
			.pipe(gulp.dest('./public/js/'))
			;
		});
	});
});

gulp.task('scripts',  ['browserify'], function () {
	gulp.src(resources.scripts.common)
		.pipe(gulp.dest('./public/js/'));
});

gulp.task('styles', function () {
	gulp.src('./assets/css/*.css')
		.pipe(gulp.dest('./public/css/'));
	gulp.src(resources.styles)
		.pipe(watch(function(files) {
			return files.pipe(less())
				.pipe(gulp.dest('./public/css/'));
		}));
});

gulp.task('images', function () {
	gulp.src(resources.images)
		.pipe(gulp.dest('./public/img/'));
});

gulp.task('migrate', function () {
	sys.puts('for now use rake');
});

// task to run while actively developing
gulp.task('watch', ['scripts','styles', 'images'], function () {
		gulp.watch(resources.scripts, ['scripts']);
		gulp.watch(resources.styles, ['styles']);
		gulp.watch(resources.images, ['images']);
});