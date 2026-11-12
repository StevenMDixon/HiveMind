import type { Media, Tag } from '../../media/types';
import type { CategoryItem } from '../types';
import type { EnumOptionsObj } from '../../../utilities/types';
import { useState } from 'react';

import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import Divider from '@mui/material/Divider';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Chip from '@mui/material/Chip';
import Stack from '@mui/material/Stack';
import IconButton from '@mui/material/IconButton';
import AddIcon from '@mui/icons-material/Add';
import CardActions from '@mui/material/CardActions';
import Button from '@mui/material/Button';
import Autocomplete from '@mui/material/Autocomplete';

import { useTagsUpdateHook } from '../hooks/queueHook';
import CustomDialog from '@/components/Dialog';
import type { CustomFormField } from '../../../components/FormFields';
import { TextField } from '@mui/material';
// interface TagSectionProps {
//     category: string;
//     required: boolean;
//     tags: Tag[];
//     handleDelete: (tag: Tag) => void;
// }

// const borderColor = (tags: Tag[], required: boolean) : string=> {
//     let color = 'grey';
    
//     if (required && tags.length == 0) color = '#D69E36';

//     return color;
// }

// const TagSection = ({ category, required, tags, handleDelete}: TagSectionProps) => {
//     if (tags.length == 0 && !required) return null;

//     return (
//         <Box sx={{ p: 2, border: `1px solid ${borderColor(tags, required)}`, borderRadius: '10px', marginBottom: '.5em' }}>
//             <Stack direction="row" sx={{justifyContent: 'space-between'}} >
//                 <Typography variant="subtitle1">
//                         {category.replace('_', ' ')}
//                 </Typography>
//                 {
//                     required && !tags.length && <Typography color='#D69E36'>Required</Typography>
//                 }
//             </Stack>
//             {
//                 tags.map(x => <Chip size="small" variant="outlined" label={x.name} onDelete={() => handleDelete(x)}/>)
//             }
//         </Box>
//     );
// }

interface ReqChipProps {
    name: string | number | boolean;
    complete: boolean;
}

const ReqChip = ({ name, complete }: ReqChipProps) => <Chip size="small" variant="outlined" color={complete ? 'success' : 'error'} label={name.replace("_", " ")} />

interface RequiredTagsProps {
    tags: Tag[];
    requiredCategories: CategoryItem[];
    categories: EnumOptionsObj;
}

const RequiredTags = ({ tags, requiredCategories, categories } : RequiredTagsProps) => {
    return (
        <Stack direction="row" sx={{ justifyContent: 'space-between' }} >
            {requiredCategories.map(x =>

                <ReqChip name={categories[x.category]} complete={tags.some(y => x.category === y.category)} />
            )}
        </Stack>
    )
}

const TagInput = ({ tags, selectedTags, catagories, setTags }) => {
    return <Autocomplete
        multiple
        options={tags}
        value={selectedTags}
        getOptionLabel={(option) => option.name + ' - ' + catagories[option.category]}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        renderInput={(params) => <TextField {...params} />}
        onChange={(event, newValue) => {
            console.log(newValue)
            setTags(newValue);
          }}
    />
}

interface MediaTaggingProps {
    media: Media;
    allCategories: EnumOptionsObj;
    tagCategories: CategoryItem[];
    availableTags: Tag[];
}

const MediaTagging = ({ media, allCategories, tagCategories, availableTags }: MediaTaggingProps) => {

    const { mutate: saveTags } = useTagsUpdateHook();

    const [tags, setTags] = useState(media.tags ?? []);

    const addTag = (tag: Tag) => setTags([...tags, {...tag, id: tags.length * -1}]);

    const removeTag = (tag: Tag) => setTags(tags.filter(t => t.id !== tag.id));

    const save = () => {
        saveTags(
            {
                ...media,
                tags: tags
            }
        )
    };

    const tagDefault = { id: 0, name: '', category: 0 } as Tag

    const fields = [
        { name: 'name', type: "Text", initialValue: '',  required: true, display: "Name" },
        { name: 'category', type: "Select", initialValue: 0,  options: allCategories,  required: false, validator: (x: string) => x != '', display: "Category" }
    ] as CustomFormField[];

    return (
        <Card sx={{maxWidth: '50%',minWidth: '50%', borderRadius: '10px'}} >
          <CardMedia
              component="video"
              image={"/api/mediaitems/" + media.id + "/stream"}
                controls
              sx={{
                width: "100%",
                objectFit: "contain",
                maxHeight: "350px"
              }}
            />
            <CardContent>
                <Typography variant="subtitle1">
                    {media.title}
                </Typography>
                <Typography variant="subtitle2">
                    Duration: {media.duration / 1000}s | Resolution: {media.resolution}
                </Typography>
                <Divider sx={{ borderColor: 'secondary.main', marginY: '.5em' }} />
                <Stack direction='row' sx={{justifyContent: 'space-between', marginY: '.5em'}} >
                <Box>
                    <Typography>
                        Classification Requirements
                    </Typography>
                   
                    </Box>
                    {/* <CustomDialog buttonText="Add Tag" title="Add Tag" save={addTag} initialValue={tagDefault} fields={fields}/> */}
                    <RequiredTags tags={tags} requiredCategories={tagCategories.filter(x => x.required)} categories={allCategories} />
            </Stack>
               {/*  {
                    tagCategories && tagCategories.sort((a, b) => b.category - a.category).map(x =>
                        <TagSection category={allCategories[x.category] as string} required={x.required} tags={tags.filter(item => x.category == item.category)} handleDelete={removeTag} >
                    </TagSection>
                    )
                } */}
                <TagInput tags={availableTags} selectedTags={tags} catagories={allCategories} setTags={addTag} />
                <CardActions>
                    <Button sx={{ width: '100%', border: '1px solid green', color: "green" }} onClick={save} >Save & Next</Button>
                </CardActions>
            </CardContent>
      </Card>
  );
}

export default MediaTagging;