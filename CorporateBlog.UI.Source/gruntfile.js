module.exports = function (grunt) {
    'use strict';

    var buildPath = '../../CorporateBlog.WebApi';

    grunt.initConfig({
        buildPath: buildPath,
        pkg: grunt.file.readJSON('package.json'),

        clean: {
            options: {
                'no-write': true
            },
            build: [
                '<%= buildPath %>/website/',
                '<%= buildPath %>/Index.html',
                '!<%= buildPath %>/website/libs',
                '!<%= buildPath %>/website/bower.json',
                '!<%= buildPath %>/website/.bowerrc'
            ]
        },

        copy: {
            build: {
                expand: true,
                dest: '<%= buildPath %>/website/',
                src: ['website', 'Index.html']
            }
        },

        less: {
            build: {
                files: {
                    '<%= buildPath %>/website/styles/app.css': 'styles/*.less'
                }
            }
        },
        
        watch: {
            scripts: {
                files: ['Index.html', 'website'],
                tasks: ['clean', 'copy', 'less', 'htmlbuild', 'wiredep'],
                options: {
                    spawn: false,
                }
            }
        },

        htmlbuild: {
            build: {
                src: 'Index.html',
                dest: '<%= buildPath %>',
                options: {
                    styles: {
                        bundle: ['app.css']
                    },

                    scripts: {
                        bundle: ['website/js/**/*.js']
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

    grunt.registerTask('default', ['copy',  'htmlbuild', 'wiredep', 'less']);
};