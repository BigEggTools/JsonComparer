# Compare the JSON files in two folder
Compare the JSON files in two folder.

## Usage

Usage of compare function.
```
JsonComparer: v0.2.0.8

Command 'compare' - Compare the JSON files in two folder.

Usage:

compare --path1 <String> --path2 <String> --config <String> [--output <String>]

Description:

    --path1  | --p1     The folder name of JSON files to compare.
    --path2  | --p2     The another folder name of JSON files to compare.
    --config | --c      The configuration file for compare action.
    --output | --o      The output folder name of the Compare Results.
```

## Example

To Compare the nodes under "mapping" node in BigJsonFile.json to "Output" folder

```
compare --path1 CompareFiles\path1 --path2 CompareFiles\path2 --config CompareFiles\config.json --output output
```

## Compare Configuration
The configuration file is used for constructing the compare item. It's contains the following properties:

| Property Name | Description | Validation |
| ------------- |-------------| ---------- |
| startNodeName | The name of node to start anaysis | Required |
| propertyNodesName | The node name to be ignore in the report, such as: "properties" | Required, Not Empty |
| fieldInfos | The configuration of node that should be the compare item's field | Required, Not Empty |

For "fieldInfos", will have 4 properties:

| Property Name | Description | Validation |
| ------------- |-------------| ---------- |
| fieldName | The name of node to become the field | Required, Not Empty String |
| fieldType | The data type of this node's value | Required, only support: "String"， "Integer", "Boolean" |
| defaultValue | When not found this field, the default value will be used to compare | Required, Not Empty String |
| replaceValue | When node's value equal to replace value, the default value will be used to compare | Not Required |

### Configuration Example
In current version, only support JSON format.

```JSON
{
  "startNodeName": "_document",
  "propertyNodesName": [
    "properties",
    "fields"
  ],
  "fieldInfos": [
    {
      "fieldName": "type",
      "fieldType": "String",
      "defaultValue": "default"
    },
    {
      "fieldName": "include_in_all",
      "fieldType": "Boolean",
      "defaultValue": false,
      "replaceValue": "default"
    },
    {
      "fieldName": "analyzer",
      "fieldType": "String",
      "defaultValue": "default"
    }
  ]
}
```
