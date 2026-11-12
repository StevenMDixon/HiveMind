import Editor from '@monaco-editor/react';

interface EditorProps {
    lineupJson: string;
    save: (data: string) => void
}

const JsonEditor = ({ lineupJson, save }: EditorProps) => {

    return (
        <div style={{
            padding: "20px"
        }}>
            <Editor
                height="800px"
                language="json"
                theme="vs-dark"
                value={lineupJson}
                onChange={(newValue) => save(newValue || '')}
                options={{ minimap: { enabled: false } }}
            />
        </div>
    )
}

export default JsonEditor;