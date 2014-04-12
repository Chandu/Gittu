var gulp = require('gulp');
var clean = require('gulp-clean');

module.exports = function(){
	gulp.task('images', function () {
		gulp.src('./img/*.*')
			.pipe(gulp.dest('../public/img/'));
	});
};

