using System;
using System.IO;

using ImageMagick;

using ShellProgressBar;

string dir = $"{new FileInfo(args[0]).DirectoryName}/Old";

if (!Directory.Exists(dir)) {
    _ = Directory.CreateDirectory(dir);
}

int totalTicks = args.Length;

ProgressBarOptions progressBarOptions = new ProgressBarOptions {
    ProgressCharacter = '─',
    ProgressBarOnBottom = true
};

ProgressBar progressBar = new(totalTicks, "Converting Images", progressBarOptions);

foreach (string file in args) {

    progressBar.Message = $"Converting {Path.GetFileName(file)}";
    //Console.WriteLine($"Converting {Path.GetFileName(file)}");
    MagickImage image = new(file);
    image.Write($"{Path.GetFileNameWithoutExtension(file)}.webp");
    //Console.WriteLine($"Done!");
    progressBar.Tick();

    File.Move(file, $"{dir}/{Path.GetFileName(file)}");
}

//Console.WriteLine("Press any button to close");
_ = Console.ReadKey();
