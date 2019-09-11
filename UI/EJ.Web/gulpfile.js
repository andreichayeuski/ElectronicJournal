var _ = require('lodash'),
    gulp = require("gulp"),
    //cleanCss = require("gulp-clean-css"),
    watch = require('gulp-watch'),
    //inject = require('gulp-inject'),
    less = require("gulp-less"),
    webpack = require('webpack');
// minify = require('gulp-minify');
//gulpCopy = require('gulp-copy');

gulp.task('watch', function () {
    gulp.watch('wwwroot/app/js/Group/Components/*.vue', gulp.series('build-scripts'));
});

gulp.task('build-scripts', done => {
    webpack(require('./webpack.config.js'),
        (err, stats) => {
            //console.log(stats);
            if (err) throw new util.PluginError('webpack', err);
        });
    done();
});


gulp.task('watch', function () {
    gulp.watch('wwwroot/app/css/*.less', ['less-to-css']);
});

gulp.task("less-to-css", function () {
    return gulp.src('wwwroot/app/css/*.less')
        .pipe(less())
        .pipe(gulp.dest('wwwroot/app/css'));
    done();
});


