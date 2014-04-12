var gulp = require('gulp');
var uglify = require('uglify-js');
var gulpif= require('gulp-if');
var browserify= require('browserify');
var glob = require('glob');
var path = require('path');
var source = require('vinyl-source-stream');
var clean = require('gulp-clean');

var scripts =  {
	'common' : ['./components/modernizr/modernizr.js'],
	'apps' : './js/app/*.app.js' 
};


module.exports = function() {

	gulp.task('browserify',  function () {

		glob(scripts.apps, null, function (er, files) {
			files.forEach(function(file) {
				browserify({
					debug: true
				}).require(file, {entry: true})
				.bundle()
				.pipe(source(path.basename(file)))
				.pipe(gulp.dest('../public/js/'))
				;
			});
		});
	});

	gulp.task('scripts',  ['browserify'], function () {
		gulp.src(scripts.common)
			.pipe(gulp.dest('../public/js/'));
	});
};