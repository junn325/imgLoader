# imgLoader_CLI
Hitomi.la / hiyobi.me / nhentai.net downloader without external libraries<br/>
hiyobi.me defalut option: try download first from Hitomi.la<br/>

```
usage:
  imgL
  imgL [flags] [urls]

flags:
  -r    Specify a temporary path
  -nh   (Only on hiyobi)Disable try download first on Hitomi.la
  
examples:
  imgL hitomi.la/readers/000000.html
  imgL -r "D:\temp for test" hitomi.la/readers/000000.html hitomi.la/readers/000000.html
  imgL -nh -r "D:\temp for test" hiyobi.me/reader/000000 hiyobi.me/reader/000000
```
<br/><br/>
# Available sites
Hitomi.la<br/>
hiyobi.me<br/>
nhentai.net<br/>
