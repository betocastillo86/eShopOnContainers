import { Component, OnInit } from '@angular/core';
import { ICatalogItem } from '../../shared/models/catalogItem.model';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../catalog.service';
import { ConfigurationService } from '../../shared/services/configuration.service';
import { CommentService } from '../comment.service';
import { ICommentList } from '../../shared/models/commentList.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IComment } from '../../shared/models/comment';

@Component({
  selector: 'app-catalog-detail',
  templateUrl: './catalog-detail.component.html',
  styleUrls: ['./catalog-detail.component.scss']
})
export class CatalogDetailComponent implements OnInit {

  public item: ICatalogItem;
  public form: FormGroup;
  public formSent: boolean;
  public comments: ICommentList;
  

  constructor(
    private activatedRoute: ActivatedRoute, 
    private catalogService: CatalogService,
    private configurationService: ConfigurationService,
    private commentService: CommentService,
    private fb: FormBuilder) { }

  ngOnInit() {
    var itemId = +this.activatedRoute.snapshot.params['id'];

    if (this.configurationService.isReady) 
      this.loadProduct(itemId);
    else
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.loadProduct(itemId);
      });

      this.form = this.fb.group({
        text: ['', Validators.required]
      });
  }

  private loadProduct(itemId: number): void{
    this.catalogService.getCatalogById(itemId).subscribe(
      data => this.loadProductCompleted(data),
      error => this.loadProductError(error)
    );
  }

  loadProductCompleted(item: ICatalogItem): void
  {
    this.item = item;
    this.loadComments();
  }

  loadComments(): void {
    this.commentService.getComments(this.item.id).subscribe(
      data => this.comments = data,
      error => console.log(error)
    )
  }

  loadProductError(error: any): void
  {
    alert("Error cargando");
  }

  saveComment():void{
    this.formSent = true;
    if(this.form.valid)
    {
      var comment = <IComment>{ text: this.form.value.text, itemId: this.item.id };
      this.commentService.post(comment).subscribe(
        data => this.commentSaved(),
        error => alert('Error guardando')
      );
    }
  }

  private commentSaved(): void {
    this.formSent = false;
    this.form.reset();
    this.loadComments();
  }

}
