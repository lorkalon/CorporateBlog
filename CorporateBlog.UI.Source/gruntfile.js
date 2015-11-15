module.exports = function (grunt) {
    'use strict';

    var buildPath = '../CorporateBlog.WebApi';

    grunt.initConfig({
        buildPath: buildPath,
        pkg: grunt.file.readJSON('package.json'),

        clean: {
            options: {
                'no-write': true,
                force: true,

            },
            build: [
                'build/',
                '!build/libs',
            ]
        },

        copy: {
            build: {
                expand: true,
                dest: 'build/',
                src: ['website/**']
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
                    'build/styles/app.css': 'styles/*.less'
                }
            }
        },
        
        watch: {
            scripts: {
                files: ['Index.html', 'website'],
                tasks: ['clean', 'less', 'htmlbuild', 'wiredep', 'copy'],
                options: {
                    spawn: false,
                }
            }
        },

        htmlbuild: {
            build: {
                src: 'Index.html',
                dest: 'build/',
                options: {
                    styles: {
                        bundle: ['build/styles/app.css']
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

    grunt.registerTask('default', ['clean', 'less', 'copy:build', 'htmlbuild', 'wiredep', 'copy:finilize']);
};