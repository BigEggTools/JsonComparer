# Merge JSON Files
Merge the splited JSON files to node in one JSON file, to build a big file.
- Can merge the Json to Object's Properties and the Json Array.
- Can concat the properties or array.

## Usage

Usage of merge function.

```
JsonComparer: v0.3.0.9

Command 'merge' - Merge the splited JSON files to node in one JSON file, to build a big file.

Usage:

merge --root_file <String> --node_name <String> --small_file_path <String>

Description:

    --root_file          | --r                  The JSON file name to be the root.
    --node_name          | --n                  The root node name for merging.
    --small_file_path    | --fp                 The splited JSON files' path.
```

## Example

To split the nodes under "mapping" node in BigJsonFile.json to "Output" folder

```
merge --r root.json --n mappings --fp items
```
