require 'albacore'
 
task :default => [:build]
 
desc "Build"
msbuild :build, [:env] do |msb, args|
	args.with_defaults(:env => 'Debug')
	msb.properties :configuration => args.env
	msb.targets :Clean, :Build
	msb.solution = "./Gittu.sln"
end

desc "Migrate Db"
fluentmigrator :migrate, [:env]  => [:build] do |migrator, args|
	 args.with_defaults(:env => 'Debug')
	configure_migrator migrator, args
end

desc "Migrate Db Down"
fluentmigrator :unmigrate, [:env] => [:build] do |migrator, args|
	 args.with_defaults(:env => 'Debug')
	configure_migrator migrator
	migrator.task = "rollback"
end

desc "Find Migrator"
task "hunt", [:Dev] do |hunt, args| 
	if ENV['CONNECTION_STRING'].nil?
		puts 'chandu'
	else
		puts ENV['CONNECTION_STRING']
	end
 end

def configure_migrator(migrator, args)
	migrator.command = Dir.glob((File.dirname(__FILE__ )<< "/packages/FluentMigrator*/tools/Migrate.exe")).first
	if ENV['CONNECTION_STRING'].nil?
		db_file = "#{File.dirname(__FILE__ )}/Gittu.Web/App_Data/gittu.db"
		if !File.exist?(db_file)
		end
		migrator.provider = 'sqlserver'
		migrator.connection = "Server=.\\SQLExpress;Database=GittuDB;Trusted_Connection=True;"
	else
		migrator.provider = ENV['CONNECTION_PROVIDER']
		migrator.connection = ENV['CONNECTION_STRING']
		
	end
	
	migrator.target = "./Gittu.Migrations/bin/#{args.env}/Gittu.Migrations.dll"
	
	migrator.verbose = true
end
