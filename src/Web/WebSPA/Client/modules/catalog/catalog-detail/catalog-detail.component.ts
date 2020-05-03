import { Component, OnInit } from '@angular/core';
import { ICatalogItem } from '../../shared/models/catalogItem.model';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../catalog.service';
import { ConfigurationService } from '../../shared/services/configuration.service';
import { CommentService } from '../comment.service';
import { ICommentList } from '../../shared/models/commentList.model';

@Component({
  selector: 'app-catalog-detail',
  templateUrl: './catalog-detail.component.html',
  styleUrls: ['./catalog-detail.component.scss']
})
export class CatalogDetailComponent implements OnInit {

  public item: ICatalogItem;
  public comments: ICommentList;

  constructor(
    private activatedRoute: ActivatedRoute, 
    private catalogService: CatalogService,
    private configurationService: ConfigurationService,
    private commentService: CommentService) { }

  ngOnInit() {
    var itemId = +this.activatedRoute.snapshot.params['id'];

    if (this.configurationService.isReady) 
      this.loadProduct(itemId);
    else
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.loadProduct(itemId);
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

}
