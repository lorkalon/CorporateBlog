﻿module.exports = function (grunt) {
    'use strict';

    var buildPath = '../../CorporateBlog.WebApi';

    grunt.initConfig({
        buildPath: buildPath,
        pkg: grunt.file.readJSON('package.json'),

        clean: {
            options: { force: true },
            build: ['<%= buildPath %>']
        },

        copy: {
            build: {
                expand: true,
                dest: buildPath,
                src: ['img/**', 'js/**', 'libs/**', 'partials/**', 'sources/**', 'views/**', 'styles/**', 'index.html']
            }
        },

        less: {
            build: {
                files: {
                    '<%= buildPath %>/app.css': 'styles/*.less'
                }
            }
        },
        
        watch: {
            scripts: {
                files: ['js/*', 'partials/*', 'sources/*', 'styles/*', 'views/*', 'Index.html'],
                tasks: ['clean', 'copy', 'htmlbuild', 'wiredep', 'less']
            }
        },

        htmlbuild: {
            build: {
                src: '<%= buildPath %>/index.html',
                dest: '<%= buildPath %>/',
                options: {
                    styles: {
                        bundle: ['app.css']
                    },

                    scripts: {
                        bundle: ['js/**/*.js']
                    }
                }
            }
        },

        uglify: {
            build: {
                '<%= buildPath %>/app.min.js': ['<%= buildPath %>/app.js', '<%= buildPath %>/modules/**/*.js']
            }
        },

        jshint: {
            files: ['gruntfile.js', 'js/**/*.js'],
            // configure JSHint (documented at http://www.jshint.com/docs/)
           
        },

        wiredep: {
            task: {
                src: [
                  '<%= buildPath %>/index.html'
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

    grunt.registerTask('default', ['clean', 'copy', 'htmlbuild', 'wiredep', 'less']);
    grunt.registerTask('build-watch', ['watch']);
};