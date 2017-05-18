# Split JSON File
Split the big JSON file to multiple small files.
- Can split the Json Object's Properties and the Json Array.

## Usage

Usage of split function.

```
JsonComparer: v0.2.0.8

Command 'split' - Split the big JSON file to multiple small files.

Usage:

split --input <String> --output <String> --node_name <String> [--output_pattern <String>]

Description:

    --input          | --i    The path of JSON file to split.
    --output         | --o    The path to store the splited JSON files.
    --node_name      | --n    The name of node to split.
    --output_pattern | --op   The output file name pattern. Use '${name}' for node name, '${index}' for the child index.
```

## Example

To split the nodes under "mapping" node in BigJsonFile.json to "Output" folder

```
split --i BigJsonFile.json --o .\Output\ --n mappings
```
