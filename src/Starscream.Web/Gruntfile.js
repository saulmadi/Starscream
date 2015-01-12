module.exports = function(grunt) {

    require('time-grunt')(grunt);

    grunt.initConfig({
        pkg: '<json:package.json>',
        concat: {
            options: {
                separator: ';'
            },
            basic_and_extras: {
                files: {
                    'app/main-app.js': [
                        'app/app.js',
                        'app/controllers/{,*/}*.js',
                        'app/directives/{,*/}*.js',
                        'app/services/{,*/}*.js'
                    ],
                    'app/main-libs.js': [
                        'assets/angular.min.js',
                        'assets/angular-route.js',
                        'assets/jquery-1.10.2.min.js',
                        'assets/bootstrap-3.1.1-dist/js/bootstrap.min.js',
                        'assets/bootstrap-multiselect/bootstrap-multiselect.js'
                    ]
                },
            },
        },
        uglify: {
            options: {
                preserveComments: false,
                compress: {
                    drop_console: true
                }
            },
            my_target: {
                files: {
                    'app/main-app.min.js': ['app/main-app.js'],
                    'app/main-libs.min.js': ['app/main-libs.js']
                }
            }
        },
        jshint: {
            files: ['app/app.js',
                'app/controllers/{,*/}*.js',
                'app/directives/{,*/}*.js',
                'app/services/{,*/}*.js'
            ],
            options: {
                maxerr: 10,
                curly: true,
                eqeqeq: true,
                eqnull: true,
                browser: true,
                devel: true,
                smarttabs: true,
                globals: {
                    exports: true,
                    module: true,
                    console: true,
                    $: true,
                    window: true,
                    angular: true
                },
            },
        },
        watch: {
            scripts: {
                files: ['app/app.js',
                    'app/controllers/{,*/}*.js',
                    'app/directives/{,*/}*.js',
                    'app/services/{,*/}*.js'
                ],
                tasks: ['concat'],
                options: {
                    spawn: false
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('deploywatch', ['jshint', 'concat', 'watch']);
    grunt.registerTask('deploy', ['jshint', 'concat']);
    grunt.registerTask('deployprod', ['jshint', 'concat', 'uglify']);
};