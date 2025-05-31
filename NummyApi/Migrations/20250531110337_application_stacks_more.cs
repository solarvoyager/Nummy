using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NummyApi.Migrations
{
    /// <inheritdoc />
    public partial class application_stacks_more : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("60974335-6296-4596-9250-702824c3920f"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("663e0065-ff2f-4dd6-9c9d-2e6c83595ada"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("85ef383f-1a89-4882-b59e-2db52bfedfe0"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("e089f7ee-7a8f-40f4-989e-316d00212d96"));

            migrationBuilder.DropColumn(
                name: "AvatarColorHex",
                table: "Applications");

            migrationBuilder.InsertData(
                table: "ApplicationStacks",
                columns: new[] { "Id", "CreatedAt", "IconSvg", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/C%23-%23239120.svg?logo=c-sharp&logoColor=white", "C#", 4 },
                    { new Guid("11111111-2222-3333-4444-555555555555"), new DateTimeOffset(new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/EJS-B4CA65?logo=ejs&logoColor=black", "EJS", 12 },
                    { new Guid("11111111-2222-3333-4444-555555555556"), new DateTimeOffset(new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Nix-5277C3?logo=nixos&logoColor=white", "Nix", 30 },
                    { new Guid("11111111-2222-3333-4444-555555555557"), new DateTimeOffset(new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/TypeScript-3178C6?logo=typescript&logoColor=white", "TypeScript", 45 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Java-%23ED8B00.svg?logo=openjdk&logoColor=white", "Java", 22 },
                    { new Guid("22222222-3333-4444-5555-666666666666"), new DateTimeOffset(new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Erlang-A90533?logo=erlang&logoColor=white", "Erlang", 13 },
                    { new Guid("22222222-3333-4444-5555-666666666667"), new DateTimeOffset(new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/OCaml-EC6813?logo=ocaml&logoColor=white", "OCaml", 31 },
                    { new Guid("22222222-3333-4444-5555-666666666668"), new DateTimeOffset(new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/V-5D87BF?logo=v&logoColor=white", "V", 46 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Flutter-02569B?logo=flutter&logoColor=white", "Flutter", 15 },
                    { new Guid("33333333-4444-5555-6666-777777777777"), new DateTimeOffset(new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/F%23-378BBA?logo=f-sharp&logoColor=white", "F#", 14 },
                    { new Guid("33333333-4444-5555-6666-777777777778"), new DateTimeOffset(new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://custom-icon-badges.demolab.com/badge/Odin-1E5184?logo=odinlang", "Odin", 32 },
                    { new Guid("33333333-4444-5555-6666-777777777779"), new DateTimeOffset(new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/WebAssembly-654FF0?logo=webassembly&logoColor=white", "WebAssembly", 47 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black", "JavaScript", 23 },
                    { new Guid("44444444-5555-6666-7777-888888888888"), new DateTimeOffset(new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Fortran-734F96?logo=fortran&logoColor=white", "Fortran", 16 },
                    { new Guid("44444444-5555-6666-7777-888888888889"), new DateTimeOffset(new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Objective--C-3A95E3?logo=apple&logoColor=white", "Objective-C", 33 },
                    { new Guid("44444444-5555-6666-7777-88888888888a"), new DateTimeOffset(new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/XML-767C52?logo=xml&logoColor=white", "XML", 48 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTimeOffset(new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/AssemblyScript-007AAC?logo=assemblyscript&logoColor=white", "AssemblyScript", 0 },
                    { new Guid("55555555-6666-7777-8888-999999999990"), new DateTimeOffset(new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Perl-39457E?logo=perl&logoColor=white", "Perl", 34 },
                    { new Guid("55555555-6666-7777-8888-999999999999"), new DateTimeOffset(new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Go-00ADD8?logo=go&logoColor=white", "Go", 17 },
                    { new Guid("55555555-6666-7777-8888-99999999999b"), new DateTimeOffset(new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/YAML-CB171E?logo=yaml&logoColor=white", "YAML", 49 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTimeOffset(new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Bash-4EAA25?logo=gnu-bash&logoColor=white", "Bash", 1 },
                    { new Guid("66666666-7777-8888-9999-aaaaaaaaaaaa"), new DateTimeOffset(new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Haskell-5E5086?logo=haskell&logoColor=white", "Haskell", 18 },
                    { new Guid("66666666-7777-8888-9999-aaaaaaaaaaab"), new DateTimeOffset(new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/PHP-777BB4?logo=php&logoColor=white", "PHP", 35 },
                    { new Guid("66666666-7777-8888-9999-aaaaaaaaaaac"), new DateTimeOffset(new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Zig-F7A41D?logo=zig&logoColor=black", "Zig", 50 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTimeOffset(new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/C-00599C?logo=c&logoColor=white", "C", 2 },
                    { new Guid("77777777-8888-9999-aaaa-bbbbbbbbbbbb"), new DateTimeOffset(new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Haxe-EA8220?logo=haxe&logoColor=white", "Haxe", 19 },
                    { new Guid("77777777-8888-9999-aaaa-bbbbbbbbbbbc"), new DateTimeOffset(new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Python-3776AB?logo=python&logoColor=white", "Python", 36 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTimeOffset(new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/C++-00599C?logo=c%2B%2B&logoColor=white", "C++", 3 },
                    { new Guid("88888888-9999-aaaa-bbbb-cccccccccccc"), new DateTimeOffset(new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/HTML-E34F26?logo=html5&logoColor=white", "HTML", 20 },
                    { new Guid("88888888-9999-aaaa-bbbb-cccccccccccd"), new DateTimeOffset(new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/R-276DC3?logo=r&logoColor=white", "R", 37 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTimeOffset(new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/CoffeeScript-2F2625?logo=coffeescript&logoColor=white", "CoffeeScript", 5 },
                    { new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd"), new DateTimeOffset(new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/HTMX-0366C?logo=htmx&logoColor=white", "HTMX", 21 },
                    { new Guid("99999999-aaaa-bbbb-cccc-ddddddddddee"), new DateTimeOffset(new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Ruby-CC342D?logo=ruby&logoColor=white", "Ruby", 38 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTimeOffset(new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Clojure-5881D8?logo=clojure&logoColor=white", "Clojure", 6 },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), new DateTimeOffset(new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/JSON-000000?logo=json&logoColor=white", "JSON", 24 },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeff"), new DateTimeOffset(new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Rust-000000?logo=rust&logoColor=white", "Rust", 39 },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeefff"), new DateTimeOffset(new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Sass-CC6699?logo=sass&logoColor=white", "Sass", 40 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTimeOffset(new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Crystal-000000?logo=crystal&logoColor=white", "Crystal", 7 },
                    { new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"), new DateTimeOffset(new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Kotlin-7F52FF?logo=kotlin&logoColor=white", "Kotlin", 25 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTimeOffset(new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/CSS-1572B6?logo=css3&logoColor=white", "CSS", 8 },
                    { new Guid("cccccccc-dddd-eeee-ffff-111111111111"), new DateTimeOffset(new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Lua-2C2D72?logo=lua&logoColor=white", "Lua", 26 },
                    { new Guid("cccccccc-dddd-eeee-ffff-111111111112"), new DateTimeOffset(new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Scratch-4D97FF?logo=scratch&logoColor=white", "Scratch", 41 },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTimeOffset(new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Dart-0175C2?logo=dart&logoColor=white", "Dart", 9 },
                    { new Guid("dddddddd-eeee-ffff-1111-222222222222"), new DateTimeOffset(new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Markdown-000000?logo=markdown&logoColor=white", "Markdown", 27 },
                    { new Guid("dddddddd-eeee-ffff-1111-222222222223"), new DateTimeOffset(new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Scala-DC322F?logo=scala&logoColor=white", "Scala", 42 },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTimeOffset(new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Elixir-4B275F?logo=elixir&logoColor=white", "Elixir", 10 },
                    { new Guid("eeeeeeee-ffff-1111-2222-333333333333"), new DateTimeOffset(new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/MDX-1B1F24?logo=mdx&logoColor=white", "MDX", 28 },
                    { new Guid("eeeeeeee-ffff-1111-2222-333333333334"), new DateTimeOffset(new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Solidity-363636?logo=solidity&logoColor=white", "Solidity", 43 },
                    { new Guid("ffffffff-1111-2222-3333-444444444444"), new DateTimeOffset(new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Nim-FFE953?logo=nim&logoColor=black", "Nim", 29 },
                    { new Guid("ffffffff-1111-2222-3333-444444444445"), new DateTimeOffset(new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Swift-F54A2A?logo=swift&logoColor=white", "Swift", 44 },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTimeOffset(new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "https://img.shields.io/badge/Elm-1293D8?logo=elm&logoColor=white", "Elm", 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555556"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555557"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666666"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666667"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666668"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-5555-6666-777777777777"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-5555-6666-777777777778"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-5555-6666-777777777779"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-6666-7777-888888888888"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-6666-7777-888888888889"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-6666-7777-88888888888a"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("55555555-6666-7777-8888-999999999990"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("55555555-6666-7777-8888-999999999999"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("55555555-6666-7777-8888-99999999999b"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("66666666-7777-8888-9999-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("66666666-7777-8888-9999-aaaaaaaaaaab"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("66666666-7777-8888-9999-aaaaaaaaaaac"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("77777777-8888-9999-aaaa-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("77777777-8888-9999-aaaa-bbbbbbbbbbbc"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("88888888-9999-aaaa-bbbb-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("88888888-9999-aaaa-bbbb-cccccccccccd"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("99999999-aaaa-bbbb-cccc-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("99999999-aaaa-bbbb-cccc-ddddddddddee"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeff"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeefff"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-dddd-eeee-ffff-111111111111"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-dddd-eeee-ffff-111111111112"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-eeee-ffff-1111-222222222222"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-eeee-ffff-1111-222222222223"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-ffff-1111-2222-333333333333"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-ffff-1111-2222-333333333334"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-1111-2222-3333-444444444444"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-1111-2222-3333-444444444445"));

            migrationBuilder.DeleteData(
                table: "ApplicationStacks",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.AddColumn<string>(
                name: "AvatarColorHex",
                table: "Applications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ApplicationStacks",
                columns: new[] { "Id", "CreatedAt", "IconSvg", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("60974335-6296-4596-9250-702824c3920f"), new DateTimeOffset(new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "<svg><!-- Flutter Icon --></svg>", "Flutter", 2 },
                    { new Guid("663e0065-ff2f-4dd6-9c9d-2e6c83595ada"), new DateTimeOffset(new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "<svg><!-- Javascript Icon --></svg>", "Javascript", 3 },
                    { new Guid("85ef383f-1a89-4882-b59e-2db52bfedfe0"), new DateTimeOffset(new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "<svg><!-- Java Icon --></svg>", "Java", 1 },
                    { new Guid("e089f7ee-7a8f-40f4-989e-316d00212d96"), new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), "<svg><!-- CSharp Icon --></svg>", "C#", 0 }
                });
        }
    }
}
