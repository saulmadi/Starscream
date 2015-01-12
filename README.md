Starscream
==========

A SPA starter using Angular, Nancy and NHibernate.


- CQRS-ready with synchronous command dispatcher.
- Domain Events already installed with BlingBag
- SpecFlow and Selenium set up and ready for Features and Acceptance Criteria (scenarios)
- Database deployer re-builds database from domain entities

### Frontend Deployment with Grunt ###
Now the frontend code will be deployed with a Grunt workflow. Steps for installing grunt are the following

1. Install Nodejs on your machine, be sure to have this set also on your environment variables. Test by typing **npm -version** on the command line.
2. Install grunt globally on your machine: **npm install -g grunt-cli**
3. Type **npm install** on your machine, be sure to be on the **Starscream.Web directory** where the Gruntfile.js is located. This will create a node_modules folder with all the grunt tasks required to work. Dependecies are listed on the package.json file.

#### Grunt Tasks ####
- concat: Task will concat target files into one single file. This way we wrap whole frontend code into one single file, avoiding inclusion of bunch of js files on the index
- jshint: Standards are awesome. JSHint task will lint code inside controllers, directives and services directories to follow javascript standards for having clean awesome code. Link with JSHint rules is the following: [http://jshint.com/docs/options/](http://jshint.com/docs/options/)
- uglify: For production the uglify task will minify code for faster loading times. Comments and console.logs instructions will not be included on minified files.
- watch: This task is really helpful, will run automatic tasks when changes on target files are made, this way we avoid running grunt instructions everytime we make some changes on the code.

#### Custom Tasks ####
- deploy : This custom task has been defined on the gruntfile and is the command that is to be called when trying to run the define grunt tasks. The deploydev task will run the following grunt tasks: jshint, concat.
- deploywatch: Will run the following grunt tasks: jshint, concat, watch
- deployprod: jshint, concat, uglify

#### Minor Code Conventions ####
From now on the 'use strict' mode is necessary on files written for frontend code. Note that for using this it has to be wrapped inside a closure, otherwise it will throw warnings and errors from the linter. Also dependencies now have to be injected using the ** inline array annotation ** to avoid issues on resulting minified code, plus is the recommended way by the Angular team.
Example: 
	```javascript
	(function(){
		'use strict';

		angular.module('Starscream.Controllers').controller('ControllerExample', ['dep1', 'dep2', function(dep1, dep2){
				//logic here
			}]);
	}());
	```

Active demo: [http://starscream-starter.azurewebsites.net/](http://starscream-starter.azurewebsites.net/)


![Starscream](http://fc09.deviantart.net/fs47/i/2009/164/2/8/Decepticon_Starscream_by_davidnery.jpg)
