rm -rf shop.db
dotnet-ef database update
npm run mix
rm -rf ./bundle.zip
zip -r ./bundle.zip ./ -x@bundleexclude.lst