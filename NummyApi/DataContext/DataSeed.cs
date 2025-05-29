using Microsoft.EntityFrameworkCore;
using NummyApi.Entitites;
using NummyShared.DTOs.Enums;

public abstract class DataSeed
{
    internal static void SeedApplicationStacks(ModelBuilder modelBuilder)
    {
        // todo replace all guids with real guids
        modelBuilder.Entity<ApplicationStack>().HasData(
            new ApplicationStack
            {
                Id = new Guid("e089f7ee-7a8f-40f4-989e-316d00212d96"),
                Type = ApplicationStackType.CSharp,
                IconSvg = "https://img.shields.io/badge/C%23-%23239120.svg?logo=c-sharp&logoColor=white",
                Title = "C#",
                CreatedAt = new DateTime(2025, 1, 1),
            },
            new ApplicationStack
            {
                Id = new Guid("85ef383f-1a89-4882-b59e-2db52bfedfe0"),
                Type = ApplicationStackType.Java,
                IconSvg = "https://img.shields.io/badge/Java-%23ED8B00.svg?logo=openjdk&logoColor=white",
                Title = "Java",
                CreatedAt = new DateTime(2025, 1, 2),
            },
            new ApplicationStack
            {
                Id = new Guid("60974335-6296-4596-9250-702824c3920f"),
                Type = ApplicationStackType.Flutter,
                IconSvg = "https://img.shields.io/badge/Flutter-02569B?logo=flutter&logoColor=white",
                Title = "Flutter",
                CreatedAt = new DateTime(2025, 1, 3),
            },
            new ApplicationStack
            {
                Id = new Guid("663e0065-ff2f-4dd6-9c9d-2e6c83595ada"),
                Type = ApplicationStackType.JavaScript,
                IconSvg = "https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black",
                Title = "JavaScript",
                CreatedAt = new DateTime(2025, 1, 4),
            },
            new ApplicationStack
            {
                Id = new Guid("a1b2c3d4-e5f6-4789-8a1b-2c3d4e5f6a7b"),
                Type = ApplicationStackType.AssemblyScript,
                IconSvg = "https://img.shields.io/badge/AssemblyScript-007AAC?logo=assemblyscript&logoColor=white",
                Title = "AssemblyScript",
                CreatedAt = new DateTime(2025, 1, 5),
            },
            new ApplicationStack
            {
                Id = new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"),
                Type = ApplicationStackType.Bash,
                IconSvg = "https://img.shields.io/badge/Bash-4EAA25?logo=gnu-bash&logoColor=white",
                Title = "Bash",
                CreatedAt = new DateTime(2025, 1, 6),
            },
            new ApplicationStack
            {
                Id = new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"),
                Type = ApplicationStackType.C,
                IconSvg = "https://img.shields.io/badge/C-00599C?logo=c&logoColor=white",
                Title = "C",
                CreatedAt = new DateTime(2025, 1, 7),
            },
            new ApplicationStack
            {
                Id = new Guid("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8g"),
                Type = ApplicationStackType.CPlusPlus,
                IconSvg = "https://img.shields.io/badge/C++-00599C?logo=c%2B%2B&logoColor=white",
                Title = "C++",
                CreatedAt = new DateTime(2025, 1, 8),
            },
            new ApplicationStack
            {
                Id = new Guid("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8g9h"),
                Type = ApplicationStackType.CoffeeScript,
                IconSvg = "https://img.shields.io/badge/CoffeeScript-2F2625?logo=coffeescript&logoColor=white",
                Title = "CoffeeScript",
                CreatedAt = new DateTime(2025, 1, 9),
            },
            new ApplicationStack
            {
                Id = new Guid("f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8g9h0i"),
                Type = ApplicationStackType.Clojure,
                IconSvg = "https://img.shields.io/badge/Clojure-5881D8?logo=clojure&logoColor=white",
                Title = "Clojure",
                CreatedAt = new DateTime(2025, 1, 10),
            },
            new ApplicationStack
            {
                Id = new Guid("a7b8c9d0-e1f2-4a3b-4c5d-6e7f8g9h0i1j"),
                Type = ApplicationStackType.Crystal,
                IconSvg = "https://img.shields.io/badge/Crystal-000000?logo=crystal&logoColor=white",
                Title = "Crystal",
                CreatedAt = new DateTime(2025, 1, 11),
            },
            new ApplicationStack
            {
                Id = new Guid("b8c9d0e1-f2a3-4b4c-5d6e-7f8g9h0i1j2k"),
                Type = ApplicationStackType.CSS,
                IconSvg = "https://img.shields.io/badge/CSS-1572B6?logo=css3&logoColor=white",
                Title = "CSS",
                CreatedAt = new DateTime(2025, 1, 12),
            },
            new ApplicationStack
            {
                Id = new Guid("c9d0e1f2-a3b4-4c5d-6e7f-8g9h0i1j2k3l"),
                Type = ApplicationStackType.Dart,
                IconSvg = "https://img.shields.io/badge/Dart-0175C2?logo=dart&logoColor=white",
                Title = "Dart",
                CreatedAt = new DateTime(2025, 1, 13),
            },
            new ApplicationStack
            {
                Id = new Guid("d0e1f2a3-b4c5-4d6e-7f8g-9h0i1j2k3l4m"),
                Type = ApplicationStackType.Elixir,
                IconSvg = "https://img.shields.io/badge/Elixir-4B275F?logo=elixir&logoColor=white",
                Title = "Elixir",
                CreatedAt = new DateTime(2025, 1, 14),
            },
            new ApplicationStack
            {
                Id = new Guid("e1f2a3b4-c5d6-4e7f-8g9h-0i1j2k3l4m5n"),
                Type = ApplicationStackType.Elm,
                IconSvg = "https://img.shields.io/badge/Elm-1293D8?logo=elm&logoColor=white",
                Title = "Elm",
                CreatedAt = new DateTime(2025, 1, 15),
            },
            new ApplicationStack
            {
                Id = new Guid("f2a3b4c5-d6e7-4f8g-9h0i-1j2k3l4m5n6o"),
                Type = ApplicationStackType.EJS,
                IconSvg = "https://img.shields.io/badge/EJS-B4CA65?logo=ejs&logoColor=black",
                Title = "EJS",
                CreatedAt = new DateTime(2025, 1, 16),
            },
            new ApplicationStack
            {
                Id = new Guid("a3b4c5d6-e7f8-4g9h-0i1j-2k3l4m5n6o7p"),
                Type = ApplicationStackType.Erlang,
                IconSvg = "https://img.shields.io/badge/Erlang-A90533?logo=erlang&logoColor=white",
                Title = "Erlang",
                CreatedAt = new DateTime(2025, 1, 17),
            },
            new ApplicationStack
            {
                Id = new Guid("b4c5d6e7-f8g9-4h0i-1j2k-3l4m5n6o7p8q"),
                Type = ApplicationStackType.FSharp,
                IconSvg = "https://img.shields.io/badge/F%23-378BBA?logo=f-sharp&logoColor=white",
                Title = "F#",
                CreatedAt = new DateTime(2025, 1, 18),
            },
            new ApplicationStack
            {
                Id = new Guid("c5d6e7f8-g9h0-4i1j-2k3l-4m5n6o7p8q9r"),
                Type = ApplicationStackType.Fortran,
                IconSvg = "https://img.shields.io/badge/Fortran-734F96?logo=fortran&logoColor=white",
                Title = "Fortran",
                CreatedAt = new DateTime(2025, 1, 19),
            },
            new ApplicationStack
            {
                Id = new Guid("d6e7f8g9-h0i1-4j2k-3l4m-5n6o7p8q9r0s"),
                Type = ApplicationStackType.Go,
                IconSvg = "https://img.shields.io/badge/Go-00ADD8?logo=go&logoColor=white",
                Title = "Go",
                CreatedAt = new DateTime(2025, 1, 20),
            },
            new ApplicationStack
            {
                Id = new Guid("e7f8g9h0-i1j2-4k3l-4m5n-6o7p8q9r0s1t"),
                Type = ApplicationStackType.Haskell,
                IconSvg = "https://img.shields.io/badge/Haskell-5E5086?logo=haskell&logoColor=white",
                Title = "Haskell",
                CreatedAt = new DateTime(2025, 1, 21),
            },
            new ApplicationStack
            {
                Id = new Guid("f8g9h0i1-j2k3-4l4m-5n6o-7p8q9r0s1t2u"),
                Type = ApplicationStackType.Haxe,
                IconSvg = "https://img.shields.io/badge/Haxe-EA8220?logo=haxe&logoColor=white",
                Title = "Haxe",
                CreatedAt = new DateTime(2025, 1, 22),
            },
            new ApplicationStack
            {
                Id = new Guid("g9h0i1j2-k3l4-4m5n-6o7p-8q9r0s1t2u3v"),
                Type = ApplicationStackType.HTML,
                IconSvg = "https://img.shields.io/badge/HTML-E34F26?logo=html5&logoColor=white",
                Title = "HTML",
                CreatedAt = new DateTime(2025, 1, 23),
            },
            new ApplicationStack
            {
                Id = new Guid("h0i1j2k3-l4m5-4n6o-7p8q-9r0s1t2u3v4w"),
                Type = ApplicationStackType.HTMX,
                IconSvg = "https://img.shields.io/badge/HTMX-0366C?logo=htmx&logoColor=white",
                Title = "HTMX",
                CreatedAt = new DateTime(2025, 1, 24),
            },
            new ApplicationStack
            {
                Id = new Guid("i1j2k3l4-m5n6-4o7p-8q9r-0s1t2u3v4w5x"),
                Type = ApplicationStackType.JSON,
                IconSvg = "https://img.shields.io/badge/JSON-000000?logo=json&logoColor=white",
                Title = "JSON",
                CreatedAt = new DateTime(2025, 1, 25),
            },
            new ApplicationStack
            {
                Id = new Guid("j2k3l4m5-n6o7-4p8q-9r0s-1t2u3v4w5x6y"),
                Type = ApplicationStackType.Kotlin,
                IconSvg = "https://img.shields.io/badge/Kotlin-7F52FF?logo=kotlin&logoColor=white",
                Title = "Kotlin",
                CreatedAt = new DateTime(2025, 1, 26),
            },
            new ApplicationStack
            {
                Id = new Guid("k3l4m5n6-o7p8-4q9r-0s1t-2u3v4w5x6y7z"),
                Type = ApplicationStackType.Lua,
                IconSvg = "https://img.shields.io/badge/Lua-2C2D72?logo=lua&logoColor=white",
                Title = "Lua",
                CreatedAt = new DateTime(2025, 1, 27),
            },
            new ApplicationStack
            {
                Id = new Guid("l4m5n6o7-p8q9-4r0s-1t2u-3v4w5x6y7z8a"),
                Type = ApplicationStackType.Markdown,
                IconSvg = "https://img.shields.io/badge/Markdown-000000?logo=markdown&logoColor=white",
                Title = "Markdown",
                CreatedAt = new DateTime(2025, 1, 28),
            },
            new ApplicationStack
            {
                Id = new Guid("m5n6o7p8-q9r0-4s1t-2u3v-4w5x6y7z8a9b"),
                Type = ApplicationStackType.MDX,
                IconSvg = "https://img.shields.io/badge/MDX-1B1F24?logo=mdx&logoColor=white",
                Title = "MDX",
                CreatedAt = new DateTime(2025, 1, 29),
            },
            new ApplicationStack
            {
                Id = new Guid("n6o7p8q9-r0s1-4t2u-3v4w-5x6y7z8a9b0c"),
                Type = ApplicationStackType.Nim,
                IconSvg = "https://img.shields.io/badge/Nim-FFE953?logo=nim&logoColor=black",
                Title = "Nim",
                CreatedAt = new DateTime(2025, 1, 30),
            },
            new ApplicationStack
            {
                Id = new Guid("o7p8q9r0-s1t2-4u3v-4w5x-6y7z8a9b0c1d"),
                Type = ApplicationStackType.Nix,
                IconSvg = "https://img.shields.io/badge/Nix-5277C3?logo=nixos&logoColor=white",
                Title = "Nix",
                CreatedAt = new DateTime(2025, 1, 31),
            },
            new ApplicationStack
            {
                Id = new Guid("p8q9r0s1-t2u3-4v4w-5x6y-7z8a9b0c1d2e"),
                Type = ApplicationStackType.OCaml,
                IconSvg = "https://img.shields.io/badge/OCaml-EC6813?logo=ocaml&logoColor=white",
                Title = "OCaml",
                CreatedAt = new DateTime(2025, 2, 1),
            },
            new ApplicationStack
            {
                Id = new Guid("q9r0s1t2-u3v4-4w5x-6y7z-8a9b0c1d2e3f"),
                Type = ApplicationStackType.Odin,
                IconSvg = "https://custom-icon-badges.demolab.com/badge/Odin-1E5184?logo=odinlang",
                Title = "Odin",
                CreatedAt = new DateTime(2025, 2, 2),
            },
            new ApplicationStack
            {
                Id = new Guid("r0s1t2u3-v4w5-4x6y-7z8a-9b0c1d2e3f4g"),
                Type = ApplicationStackType.ObjectiveC,
                IconSvg = "https://img.shields.io/badge/Objective--C-3A95E3?logo=apple&logoColor=white",
                Title = "Objective-C",
                CreatedAt = new DateTime(2025, 2, 3),
            },
            new ApplicationStack
            {
                Id = new Guid("s1t2u3v4-w5x6-4y7z-8a9b-0c1d2e3f4g5h"),
                Type = ApplicationStackType.Perl,
                IconSvg = "https://img.shields.io/badge/Perl-39457E?logo=perl&logoColor=white",
                Title = "Perl",
                CreatedAt = new DateTime(2025, 2, 4),
            },
            new ApplicationStack
            {
                Id = new Guid("t2u3v4w5-x6y7-4z8a-9b0c-1d2e3f4g5h6i"),
                Type = ApplicationStackType.PHP,
                IconSvg = "https://img.shields.io/badge/PHP-777BB4?logo=php&logoColor=white",
                Title = "PHP",
                CreatedAt = new DateTime(2025, 2, 5),
            },
            new ApplicationStack
            {
                Id = new Guid("u3v4w5x6-y7z8-4a9b-0c1d-2e3f4g5h6i7j"),
                Type = ApplicationStackType.Python,
                IconSvg = "https://img.shields.io/badge/Python-3776AB?logo=python&logoColor=white",
                Title = "Python",
                CreatedAt = new DateTime(2025, 2, 6),
            },
            new ApplicationStack
            {
                Id = new Guid("v4w5x6y7-z8a9-4b0c-1d2e-3f4g5h6i7j8k"),
                Type = ApplicationStackType.R,
                IconSvg = "https://img.shields.io/badge/R-276DC3?logo=r&logoColor=white",
                Title = "R",
                CreatedAt = new DateTime(2025, 2, 7),
            },
            new ApplicationStack
            {
                Id = new Guid("w5x6y7z8-a9b0-4c1d-2e3f-4g5h6i7j8k9l"),
                Type = ApplicationStackType.Ruby,
                IconSvg = "https://img.shields.io/badge/Ruby-CC342D?logo=ruby&logoColor=white",
                Title = "Ruby",
                CreatedAt = new DateTime(2025, 2, 8),
            },
            new ApplicationStack
            {
                Id = new Guid("x6y7z8a9-b0c1-4d2e-3f4g-5h6i7j8k9l0m"),
                Type = ApplicationStackType.Rust,
                IconSvg = "https://img.shields.io/badge/Rust-000000?logo=rust&logoColor=white",
                Title = "Rust",
                CreatedAt = new DateTime(2025, 2, 9),
            },
            new ApplicationStack
            {
                Id = new Guid("y7z8a9b0-c1d2-4e3f-4g5h-6i7j8k9l0m1n"),
                Type = ApplicationStackType.Sass,
                IconSvg = "https://img.shields.io/badge/Sass-CC6699?logo=sass&logoColor=white",
                Title = "Sass",
                CreatedAt = new DateTime(2025, 2, 10),
            },
            new ApplicationStack
            {
                Id = new Guid("z8a9b0c1-d2e3-4f4g-5h6i-7j8k9l0m1n2o"),
                Type = ApplicationStackType.Scratch,
                IconSvg = "https://img.shields.io/badge/Scratch-4D97FF?logo=scratch&logoColor=white",
                Title = "Scratch",
                CreatedAt = new DateTime(2025, 2, 11),
            },
            new ApplicationStack
            {
                Id = new Guid("a9b0c1d2-e3f4-4g5h-6i7j-8k9l0m1n2o3p"),
                Type = ApplicationStackType.Scala,
                IconSvg = "https://img.shields.io/badge/Scala-DC322F?logo=scala&logoColor=white",
                Title = "Scala",
                CreatedAt = new DateTime(2025, 2, 12),
            },
            new ApplicationStack
            {
                Id = new Guid("b0c1d2e3-f4a5-4b6c-8d9e-0a1b2c3d4e5f"),
                Type = ApplicationStackType.Solidity,
                IconSvg = "https://img.shields.io/badge/Solidity-363636?logo=solidity&logoColor=white",
                Title = "Solidity",
                CreatedAt = new DateTime(2025, 2, 13),
            },
            new ApplicationStack
            {
                Id = new Guid("c1d2e3f4-a5b6-4c7d-8e9f-0a1b2c3d4e5f"),
                Type = ApplicationStackType.Swift,
                IconSvg = "https://img.shields.io/badge/Swift-F54A2A?logo=swift&logoColor=white",
                Title = "Swift",
                CreatedAt = new DateTime(2025, 2, 14),
            },
            new ApplicationStack
            {
                Id = new Guid("d2e3f4a5-b6c7-4d8e-9f0a-1b2c3d4e5f6a"),
                Type = ApplicationStackType.TypeScript,
                IconSvg = "https://img.shields.io/badge/TypeScript-3178C6?logo=typescript&logoColor=white",
                Title = "TypeScript",
                CreatedAt = new DateTime(2025, 2, 15),
            },
            new ApplicationStack
            {
                Id = new Guid("e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b"),
                Type = ApplicationStackType.V,
                IconSvg = "https://img.shields.io/badge/V-5D87BF?logo=v&logoColor=white",
                Title = "V",
                CreatedAt = new DateTime(2025, 2, 16),
            },
            new ApplicationStack
            {
                Id = new Guid("f4a5b6c7-d8e9-4f0a-1b2c-3d4e5f6a7b8c"),
                Type = ApplicationStackType.WebAssembly,
                IconSvg = "https://img.shields.io/badge/WebAssembly-654FF0?logo=webassembly&logoColor=white",
                Title = "WebAssembly",
                CreatedAt = new DateTime(2025, 2, 17),
            },
            new ApplicationStack
            {
                Id = new Guid("a5b6c7d8-e9f0-4a1b-2c3d-4e5f6a7b8c9d"),
                Type = ApplicationStackType.XML,
                IconSvg = "https://img.shields.io/badge/XML-767C52?logo=xml&logoColor=white",
                Title = "XML",
                CreatedAt = new DateTime(2025, 2, 18),
            },
            new ApplicationStack
            {
                Id = new Guid("b6c7d8e9-f0a1-4b2c-3d4e-5f6a7b8c9d0e"),
                Type = ApplicationStackType.YAML,
                IconSvg = "https://img.shields.io/badge/YAML-CB171E?logo=yaml&logoColor=white",
                Title = "YAML",
                CreatedAt = new DateTime(2025, 2, 19),
            },
            new ApplicationStack
            {
                Id = new Guid("i7j8k9l0-m1n2-4o3p-4q5r-6s7t8u9v0w1x"),
                Type = ApplicationStackType.Zig,
                IconSvg = "https://img.shields.io/badge/Zig-F7A41D?logo=zig&logoColor=black",
                Title = "Zig",
                CreatedAt = new DateTime(2025, 2, 20),
            }
        );
    }
}


