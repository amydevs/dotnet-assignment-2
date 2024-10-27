#!/usr/bin/env sh
rm -f PetShop/shop.db
cp PetShop/shop.db.bak PetShop/shop.db
cd PetShop
npm run mix
cd -
rm -f ./bundle.zip
zip -r ./bundle.zip ./ -x@bundleexclude.lst