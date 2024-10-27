rm -f shop.db
cp shop.db.bak shop.db
npm run mix
rm -f ./bundle.zip
zip -r ./bundle.zip ./ -x@bundleexclude.lst