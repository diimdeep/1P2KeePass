install:
	brew cask install mono-mdk

update: update_deps update_keepass

# download libs
update_deps:
	mozroots --import --sync --quiet
	mono ./.nuget/NuGet.exe restore .nuget/packages.config -PackagesDirectory packages
	cp packages/Newtonsoft.Json.10.0.2/lib/net20/Newtonsoft.Json.dll 1P2KeePass/Newtonsoft.Json.dll

# download keepass
update_keepass:
	mkdir -p temp/
	cd temp/ && wget -nc https://downloads.sourceforge.net/keepass/KeePass%202.x/2.35/KeePass-2.35.zip
	mkdir -p KeePassDistribution
	rm -rf KeePassDistribution/*
	unzip temp/KeePass-2.35.zip -d KeePassDistribution/

# build plugin as .dll
build:
	msbuild ./1P2KeePass.sln /property:Configuration=Debug /nologo /verbosity:normal

# compile plugin as .plgx
release:
	#  specifying path "1P2KeePass/" in mono gives error
	rm -rf 1P2KeePass/obj
	mono KeePassDistribution/KeePass.exe --plgx-create

# zip .plgx
distrib:
	zip $(shell ./get_version.sh)".zip" 1P2KeePass.plgx

# run keepass with .plgx
run_debug: build
	rm -f KeePassDistribution/1P2KeePass.plgx
	mono KeePassDistribution/KeePass.exe TestData/Test.kdbx

# run keepass with .dll
run_release:
	rm -f KeePassDistribution/Newtonsoft.Json.dll KeePassDistribution/_1Password2KeePass.*
	cp 1P2KeePass.plgx KeePassDistribution/1P2KeePass.plgx
	mono KeePassDistribution/KeePass.exe TestData/Test.kdbx

publish:
	hub release create -d -m $(shell ./get_version.sh) -a $(shell ./get_version.sh)".zip" $(shell ./get_version.sh)