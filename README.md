# ImpinjRxxxTool

LLRP, ftp, ssh を使用して Impinj 社製リーダのデータの取得と操作を行う.  
ftp を使用する場合は 事前に RShell にてリーダの ftp 機能を有効にする必要がある.

![Impinj Rxxx Tool](https://raw.githubusercontent.com/Livenga/ImpinjRxxxTool/master/pictures/main.png "main")


## Ftp
上図の場合, Hostname に該当するリーダ内の `/mnt/spp/log` に含まれる `syslog-*.log*` データを zip ファイルで任意の場所に保存を行う.

#### NOTE
ダウンロードする対象がファイルかディレクトリかの判断を行っていないため, ダウンロード先にディレクトリが含まれている場合エラーが発生する.
![Ftp download](https://raw.githubusercontent.com/Livenga/ImpinjRxxxTool/master/pictures/ftp_download.png "Ftp Download")


## Ssh
RShell の実行

![Ssh Result](https://raw.githubusercontent.com/Livenga/ImpinjRxxxTool/master/pictures/ssh_result.png "Ssh Result")
![Ssh Help](https://raw.githubusercontent.com/Livenga/ImpinjRxxxTool/master/pictures/ssh_help.png "RShell Help")
