find . -name "*.cs" ! -path "./obj/*" ! -path "*/App.xaml.cs" ! -path "*/AssemblyInfo.cs" ! -path "*.Designer.cs" |xargs wc -l |grep total
