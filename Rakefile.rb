# encoding: utf-8
require 'rubygems'
require 'albacore'
require 'rake/clean'

SOLUTION_FILE = 'src/Gittu.sln'
CONFIGURATION = 'Release'

Albacore.configure do |config|
	config.log_level = :verbose
	config.msbuild.use :net4
end

desc "Compiles solution"
task :default => [:clean, :compile]

desc "Compile solution file"
msbuild :compile do |msb|
  msb.properties = { :configuration => CONFIGURATION, "VisualStudioVersion" => ENV['VS110COMNTOOLS'] ? "11.0" : "10.0" }
  msb.targets :Clean, :Build
  msb.solution = SOLUTION_FILE
end