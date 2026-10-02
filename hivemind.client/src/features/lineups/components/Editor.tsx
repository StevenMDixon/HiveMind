import { useState } from 'react';
import Editor from '@monaco-editor/react';

import { Button } from "@mui/material";

interface EditorProps {
    lineupJson: string;
    save: (data: string) => void
}

const JsonEditor = ({ lineupJson, save }: EditorProps) => {

    const [value, setValue] = useState(lineupJson);

    return (
        <div style={{
            padding: "20px"
        }}>
            <Button color="secondary" onClick={() =>save(value)} > Save</Button> 
            <Editor
                height="800px"
                language="json"
                theme="vs-dark"
                value={value}
                onChange={(newValue) => setValue(newValue || '')}
                options={{ minimap: { enabled: false } }}
            />
        </div>
    )
}

export default JsonEditor;