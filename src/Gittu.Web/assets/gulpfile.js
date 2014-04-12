var defaultTasks = [ 'scripts', 'styles', 'images'];
var gulp = require('./gulp-tasks')(defaultTasks);


gulp.task('default', defaultTasks);