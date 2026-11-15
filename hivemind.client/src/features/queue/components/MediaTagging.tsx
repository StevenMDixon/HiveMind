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
import Select from '@mui/material/Select';
import type { SelectChangeEvent } from '@mui/material/Select';

import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';

import { useTagsUpdateHook } from '../hooks/queueHook';


import { NewTagModal } from './NewTagModal';

interface RequiredTagsProps {
    tags: Tag[];
    requiredCategories: CategoryItem[];
    categories: EnumOptionsObj;
}

const formatCategoryName = (id: number, allCategories: EnumOptionsObj) => {
    const name = allCategories[id] as string;
    return name.replace("_", " ");
}

const RequiredTags = ({ tags, requiredCategories, categories } : RequiredTagsProps) => {

    return (
        <Stack direction="row" sx={{ justifyContent: 'space-between' }} >
            {requiredCategories.map((x, i) => {
                    return <Typography variant="caption" color={tags.some(y => y.category == x.category)? 'green': 'red'}>{formatCategoryName(x.category, categories)}{i !== requiredCategories.length -1 ? " |": ""}</Typography>
                }
            )}
        </Stack>
    )
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

    const [selectedTag, setSelectedTag] = useState(0);

    const [modalOpen, setModalOpen] = useState(false);

    const addTag = (tag: Tag) => setTags([...tags, tag]);

    const removeTag = (tag: Tag) => {

        setTags(tags.filter(t => t.id !== tag.id));
    }

    const handleSelectTag = (e: SelectChangeEvent<number>) => {
        const tagId = e.target?.value;    
        setSelectedTag(tagId);
    }

    const handleCreateNewTag = (tag: Tag) => {
        addTag(tag);
        setModalOpen(false);
    }

    const handleAddTag = () => {
        if(selectedTag == 0) {
            setModalOpen(true); 
            return
        }
        
        const tag = availableTags.filter(x => x.id === selectedTag)[0];
        
        addTag(tag);

        setSelectedTag(0);
    }

    const handleCloseModal = () => setModalOpen(false);

    const save = () => {
        saveTags(
            {
                ...media,
                tags: tags
            }
        )
    };

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
                    <RequiredTags tags={tags} requiredCategories={tagCategories.filter(x => x.required)} categories={allCategories} />
            </Stack>
                <Box sx={{border: "1px solid gray", minHeight: "100px", padding: '1em'}}>
                    {tags.map(x => <Chip size="small" label={x.name + " - "  + allCategories[x.category]} onDelete={() => removeTag(x)}/>)}
                </Box>
                <Stack direction="row" sx={{marginTop: ".5em"}}>
                    <Select sx={{width: "60%"}} value={selectedTag} onChange={handleSelectTag}>
                        <MenuItem value={0}>Create New</MenuItem>
                        { availableTags.filter(x => !tags.map(z => z.id).includes(x.id)).map(y => <MenuItem value={y.id}>{y.name} - {formatCategoryName(y.category, allCategories)}</MenuItem>)}
                    </Select>
                    <Button sx={{width: "40%",  marginLeft: '1em'}} variant="outlined" onClick={handleAddTag} > Add Tag</Button>
                </Stack>
               
                    <Button sx={{ width: '100%', border: '1px solid green', color: "green", marginTop: '1em'}} onClick={save} >Save & Next</Button>
                <NewTagModal modalOpen={modalOpen} existingTags={availableTags} categories={allCategories} modalSave={handleCreateNewTag} modalClose={handleCloseModal}/>
            </CardContent>
      </Card>
  );
}

export default MediaTagging;