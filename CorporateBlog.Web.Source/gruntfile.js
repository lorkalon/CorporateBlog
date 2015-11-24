module.exports = function (grunt) {
    'use strict';

    var buildPath = '../CorporateBlog.WebApi';

    grunt.initConfig({
        buildPath: buildPath,
        pkg: grunt.file.readJSON('package.json'),

        clean: {
            options: {
                force: true,
            },
            build: [
                'build/website',
                'build/Index.html',
                '!build/website/libs/**'
            ],
            finilize: [
                '../CorporateBlog.WebApi/website',
                 '../CorporateBlog.WebApi/Index.html',
                '!../CorporateBlog.WebApi/website/libs/**'
            ]
        },

        copy: {
            build: {
                expand: true,
                dest: 'build/',
                src: ['website/**', '!**/*.less']
            },
            finilize: {
                expand: true,
                cwd: 'build/',
                dest: '<%= buildPath %>',
                src: ['website/**', 'Index.html']
            }
        },

        less: {
            build: {
                files: {
                    'build/website/styles/app.css': 'website/styles/*.less'
                }
            }
        },
        
        //watch: {
        //    scripts: {
        //        files: ['Index.html', 'website'],
        //        tasks: ['default'],
        //        options: {
        //            spawn: false,
        //        }
        //    }
        //},

        htmlbuild: {
            build: {
                src: 'Index.html',
                dest: 'build/',
                options: {
                    styles: {
                        bundle: ['build/website/styles/**/*.css']
                    },

                    scripts: {
                        bundle: ['build/website/js/**/*.js']
                    }
                }
            }
        },

        //uglify: {
        //    build: {
        //        '<%= buildPath %>app.min.js': ['<%= buildPath %>/app.js', '<%= buildPath %>/modules/**/*.js']
        //    }
        //},

        jshint: {
            files: ['gruntfile.js', 'website/js/*.js'],
            // configure JSHint (documented at http://www.jshint.com/docs/)
           
        },

        wiredep: {
            task: {
                src: [
                  'build/index.html'
                ],

                options: {
                    overrides: {
                        'angular-ui-bootstrap-bower': {
                            main: ['./ui-bootstrap.js', './ui-bootstrap-tpls.js']
                        }
                    }
                    // See wiredep's configuration documentation for the options
                    // you may pass:

                    // https://github.com/taptapship/wiredep#configuration
                }
            } 
        }
    });

    grunt.loadNpmTasks('grunt-wiredep');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-html-build');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('default', ['clean:build', 'copy:build', 'less', 'htmlbuild', 'wiredep', 'clean:finilize', 'copy:finilize']);
};