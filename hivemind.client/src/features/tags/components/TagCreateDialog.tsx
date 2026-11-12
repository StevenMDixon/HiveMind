import { type CustomFormField } from '../../../components/FormFields';
import CustomDialog from '../../../components/Dialog';

import type { Tag } from '../types';
import type { EnumOptionsObj } from '../../../utilities/types';

interface NewTagFormProps {
    createTag: (a: Tag) => void;
    categories?: EnumOptionsObj;
}

const TagCreateDialog = ({ createTag, categories }: NewTagFormProps) => {
    const tagDefault = { id: 0, name: '', category: '0'};

    const fields = [
        { name: 'name', display: "Name", type: "Text", initialValue: tagDefault.name, validator: (name: string) => name != '', required: true },
        { name: 'category', display: "Category", type: "Select", initialValue: tagDefault.category, required: true, options: categories },
    ] as CustomFormField[];

    return (
        <CustomDialog buttonText="Add Tag" title="Create Tag" save= {createTag} fields={ fields } initialValue={ tagDefault } />
    )
}

export default TagCreateDialog;