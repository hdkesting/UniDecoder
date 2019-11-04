import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TextComponent } from './text/text.component';
import { NameComponent } from './name/name.component';
import { BlockComponent } from './block/block.component';
import { CategoryComponent } from './category/category.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'text', component: TextComponent },
    { path: 'name', redirectTo: 'name/' },
    { path: 'name/:name', component: NameComponent },
    { path: 'block', redirectTo: 'block/' },
    { path: 'block/:block', component: BlockComponent },
    { path: 'cat', redirectTo: 'cat/' },
    { path: 'cat/:cat', component: CategoryComponent },
];

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
